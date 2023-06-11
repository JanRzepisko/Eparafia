using Eparafia.Application.Services.Jwt;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Application.Services;
using Eparafia.Identity.Domain.Entities;
using MediatR;
using Shared.BaseModels.Exceptions;
using Shared.BaseModels.Jwt;
using Shared.Service.Interfaces;

namespace Eparafia.Identity.Application.Actions.Priest;

public static class LoginPriest
{
    public sealed record Query(string Email, string Password) : IRequest<GeneratedToken>;

    public class Handler : IRequestHandler<Query, GeneratedToken>
    {
        private readonly IJwtAuth _jwtAuth;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorizationCache _authorizationCache;

        public Handler(IUnitOfWork unitOfWork, IJwtAuth jwtAuth, IAuthorizationCache authorizationCache)
        {
            _unitOfWork = unitOfWork;
            _jwtAuth = jwtAuth;
            _authorizationCache = authorizationCache;
        }

        public async Task<GeneratedToken> Handle(Query request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByLoginAsync(request.Email, cancellationToken);
            if (priest is null) throw new EntityNotFoundException("User not found");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, priest.PasswordHash)) throw new BadPassword("Bad password");
              
            if (!priest.IsActive)
            {
                //throw new InvalidRequestException("Priest is not active");
            }
            
            await _authorizationCache.CreateUser(priest.Id, priest.ParishId, cancellationToken);
            await _unitOfWork.UserSessions.AddAsync(new UserSession()
            {
                UserId = priest.Id,
                ParishId = priest.ParishId
            }, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return await _jwtAuth.GenerateJwt(priest, JwtPolicies.Priest);
        }
    }
}