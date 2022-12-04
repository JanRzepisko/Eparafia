using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Services.UserProvider;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;

namespace Eparafia.Application.Actions.UserAuth.Command;

public static class JoinToParishUser
{
    public sealed record Command(Guid ParishId) : IRequest<Unit>;

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
            var parish = await _unitOfWork.Parishes.GetByIdAsync(request.ParishId, cancellationToken);
            if (parish is null)
            {
                throw new EntityNotFoundException("Parish not found");
            }
            
            User? user = await _unitOfWork.Users.GetByIdAsync(_userProvider.Id, cancellationToken);
            user.ParishId = parish.Id;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.ParishId).NotEqual(Guid.Empty);
            }
        }
    }
}