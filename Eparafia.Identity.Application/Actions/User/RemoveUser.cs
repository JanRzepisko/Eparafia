using Eparafia.Identity.Application.DataAccess;
using FluentValidation;
using MediatR;
using Shared.BaseModels.Exceptions;
using Shared.Service.Interfaces;

namespace Eparafia.Identity.Application.Actions.User;

public static class RemoveUser
{
    public sealed record Command : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Users.ExistsAsync(_userProvider.Id, cancellationToken);
            if (!exist) throw new EntityNotFoundException("User not found");

            _unitOfWork.Users.RemoveById(_userProvider.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}