<<<<<<< HEAD:Eparafia.Identity.Application/Actions/Priest/RefreshToken.cs
using Eparafia.Application.Services.Jwt;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Application.Services;
=======
using Eparafia.Application.DataAccess;
using Eparafia.Application.Services.Jwt;
>>>>>>> 32e57d6103043468352874e6f94a66b6e6be5c7e:Eparafia.Application/Actions/Priest/Query/RefreshToken.cs
using MediatR;
using Shared.BaseModels.Exceptions;
using Shared.BaseModels.Jwt;
using Shared.Service.Interfaces;

namespace Eparafia.Identity.Application.Actions.Priest;

public static class RefreshToken
{
    public sealed record Query(string Email, string Password) : IRequest<GeneratedToken>;

    public class Handler : IRequestHandler<Query, GeneratedToken>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtAuth _jwtAuth;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IJwtAuth jwtAuth, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtAuth = jwtAuth;
            _userProvider = userProvider;
        }

        public async Task<GeneratedToken> Handle(Query request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);
            if (priest is null)
            {
                throw new EntityNotFoundException("Priest not found");
            }

            return await _jwtAuth.GenerateJwt(priest, JwtPolicies.Priest);
        }
    }
}