using Eparafia.Application.DataAccess;
using FluentValidation;
using MediatR;

namespace Eparafia.Application.Actions.User.Command;

public static class RemoveUser
{
    public sealed record Command(Guid UserId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            _unitOfWork.Users.RemoveById(request.UserId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}