using Eparafia.Application.DataAccess;
using Eparafia.Application.Services.Jwt;
using Eparafia.Infrastructure.Exceptions;
using MediatR;

namespace Eparafia.Application.Actions.UserAuth.Query;

public static class Login
{
    public sealed record Query(string Email, string Password) : IRequest<GeneratedToken>;

    public class Handler : IRequestHandler<Query, GeneratedToken>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtAuth _jwtAuth;

        public Handler(IUnitOfWork unitOfWork, IJwtAuth jwtAuth)
        {
            _unitOfWork = unitOfWork;
            _jwtAuth = jwtAuth;
        }

        public async Task<GeneratedToken> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByLoginAsync(request.Email, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new BadPassword($"Bad password");
            }

            return await _jwtAuth.GenerateJwt(user);
        }
    }
}