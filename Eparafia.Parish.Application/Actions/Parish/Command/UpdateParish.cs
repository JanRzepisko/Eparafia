using Eparafia.Application.DataAccess;
using Eparafia.Domain.Enums;
using Eparafia.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Parish.Command;

public static class UpdateParish
{
    public sealed record Command : IRequest<Unit>
    {
        public string? CallName { get; set; }
        public Address? Address { get; set; }
        public Contact? Contact { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);
            if (priest is null) throw new InvalidRequestException("Priest not found");
            if (priest.FunctionParish != FunctionParish.Owner)
                throw new InvalidRequestException("You are not the owner of the parish");

            priest.Parish.CallName = request.CallName ?? priest.Parish.CallName;
            priest.Parish.Address = request.Address ?? priest.Parish.Address;
            priest.Parish.Contact = request.Contact ?? priest.Parish.Contact;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}