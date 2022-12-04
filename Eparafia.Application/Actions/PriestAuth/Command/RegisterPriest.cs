using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Services.Jwt;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.PriestAuth.Command;

public static class RegisterPriest
{
    public sealed record Command(string Name, string Surname, string Email, string Password, string ConfirmPassword) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByLoginAsync(request.Email, cancellationToken);
            if(priest != null)
            {
                throw new Exception("User already exists");
            }

            var newPriest = new Priest
            {
                Email = request.Email,
                Id = Guid.NewGuid(),
                Name = request.Name,
                Surname = request.Surname,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                HasAvatar = false,
                IsActive = false,
            };

            await _unitOfWork.Priests.AddAsync(newPriest, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Name).MinimumLength(3).MaximumLength(20);
                RuleFor(c => c.Surname).MinimumLength(3).MaximumLength(20);
                RuleFor(c => c.Email).EmailAddress();
                RuleFor(c => c.Password).Equal(c => c.ConfirmPassword);
                RuleFor(c => c.Password)
                    .MinimumLength(8)
                    .Matches("[A-Z]")
                    .Matches("[a-z]")
                    .Matches("[0-9]")
                    .Matches("[^a-zA-Z0-9]");
            }
        }
    }
}