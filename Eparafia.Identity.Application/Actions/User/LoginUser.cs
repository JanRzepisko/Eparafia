using Eparafia.Application.Services.Jwt;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Application.Services;
using MediatR;
using Shared.BaseModels.Exceptions;
using Shared.BaseModels.Jwt;

namespace Eparafia.Identity.Application.Actions.User;

public static class LoginUser
{
    public sealed record Query(string Email, string Password) : IRequest<GeneratedToken>;

    public class Handler : IRequestHandler<Query, GeneratedToken>
    {
        private readonly IJwtAuth _jwtAuth;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IJwtAuth jwtAuth)
        {
            _unitOfWork = unitOfWork;
            _jwtAuth = jwtAuth;
        }

        public async Task<GeneratedToken> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByLoginAsync(request.Email, cancellationToken);
            if (user is null) throw new EntityNotFoundException("User not found");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) throw new BadPassword("Bad password");
            if (!user.IsActive)
            {
                //throw new InvalidRequestException("User is not active");
            }

            return await _jwtAuth.GenerateJwt(user, JwtPolicies.User);
        }
    }
}