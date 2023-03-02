using Eparafia.Application.DataAccess;
using Eparafia.Application.Enums;
using Eparafia.Domain.Enums;
using Eparafia.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Parish.Command;

public static class CreateParish
{
    public sealed record Command(string CallName, Address Address, Contact Contact) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUserProvider _userProvider;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            Domain.Entities.Priest priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);

            Guid id = Guid.NewGuid();
            
            Domain.Entities.Parish parish = new Domain.Entities.Parish()
            {
                CallName = request.CallName,
                ShortName = request.CallName.ToLower().Trim(' ') + request.Address.City.ToLower().Trim(' '),
                Contact = request.Contact,
                Priests = new List<Domain.Entities.Priest> { priest },
                Address = request.Address,
                Id = id
            };

            priest.ParishId = id;
            priest.FunctionParish = FunctionParish.Owner;
            await _unitOfWork.Parishes.AddAsync(parish, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.CallName).MinimumLength(3).MaximumLength(50);
            }
        }
    }
}
