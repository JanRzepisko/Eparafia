using Eparafia.Application.Services.Jwt;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Application.Services;
using MediatR;
using Shared.BaseModels.Exceptions;
using Shared.BaseModels.Jwt;

namespace Eparafia.Identity.Application.Actions.Priest;

public static class LoginPriest
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
            var priest = await _unitOfWork.Priests.GetByLoginAsync(request.Email, cancellationToken);
            if (priest is null) throw new EntityNotFoundException("User not found");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, priest.PasswordHash)) throw new BadPassword("Bad password");
            if (!priest.IsActive)
            {
                throw new InvalidRequestException("Priest is not active");
            }

            return await _jwtAuth.GenerateJwt(priest, JwtPolicies.Priest);
        }
    }
}