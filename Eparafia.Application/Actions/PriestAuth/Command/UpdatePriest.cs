using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.PriestAuth.Command;

public static class UpdatePriest
{
    public sealed record Command(User User) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.User.Id, cancellationToken);

            if (user is null)
            {
                throw new  EntityNotFoundException($"user with id {request.User.Id} not found");
            }
            user.Name = request.User.Name ?? user.Name;
            user.Surname = request.User.Surname ?? user.Surname;
            user.Email = request.User.Email ?? user.Name;
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.User.Id).NotEqual(Guid.Empty);
            }
        }
    }
}