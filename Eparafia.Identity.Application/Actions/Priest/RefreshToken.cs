using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Application.Services;
using Eparafia.Application.Services.Jwt;
using MediatR;
using Shared.BaseModels.Exceptions;
using Shared.BaseModels.Jwt;
using Shared.Service.Interfaces;

namespace Eparafia.Identity.Application.Actions.Priest;

public static class RefreshToken
{
    public sealed record Query : IRequest<GeneratedToken>;

    public class Handler : IRequestHandler<Query, GeneratedToken>
    {
        private readonly IJwtAuth _jwtAuth;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;
        private readonly IAuthorizationCache _authorizationCache;
        public Handler(IUnitOfWork unitOfWork, IJwtAuth jwtAuth, IUserProvider userProvider, IAuthorizationCache authorizationCache)
        {
            _unitOfWork = unitOfWork;
            _jwtAuth = jwtAuth;
            _userProvider = userProvider;
            _authorizationCache = authorizationCache;
        }

        public async Task<GeneratedToken> Handle(Query request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.UserId, cancellationToken);
            
            if (priest is null) throw new EntityNotFoundException("Priest not found");
            
            _authorizationCache.CreateUser(priest.Id, priest.ParishId, cancellationToken);
            return await _jwtAuth.GenerateJwt(priest, JwtPolicies.Priest);
        }
    }
}