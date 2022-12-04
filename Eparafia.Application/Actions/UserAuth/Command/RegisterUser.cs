using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Services.Jwt;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.UserAuth.Command;

public static class RegisterUser
{
    public sealed record Command(string Name, string Surname, string Email, string Password, string ConfirmPassword) : IRequest<Unit>;

    public sealed class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByLoginAsync(request.Email, cancellationToken);
            if(user is not null)
            {
                throw new EntityNotFoundException("User already exists");
            }

            var newUser = new User
            {
                Email = request.Email,
                Id = Guid.NewGuid(),
                Name = request.Name,
                Surname = request.Surname,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                HasAvatar = false,
                IsActive = false,
                Role = JwtPolicies.User,
                ParishId = null
            };

            await _unitOfWork.Users.AddAsync(newUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken); 
            return Unit.Value;
        }
    }
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name).MinimumLength(3).MaximumLength(20).WithMessage("Name must be between 3 and 20 characters");
            RuleFor(c => c.Surname).MinimumLength(3).MaximumLength(20).WithMessage("Surname must be between 3 and 20 characters");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Invalid email address");
            RuleFor(c => c.Password).Equal(c => c.ConfirmPassword).WithMessage("Passwords must be the same");
            RuleFor(c => c.Password)
                .MinimumLength(8)
                .Matches("[A-Z]")
                .Matches("[a-z]")
                .Matches("[0-9]")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one number and one special character");
        }
    }
}


