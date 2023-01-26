using Eparafia.Application.DataAccess;
using Eparafia.Application.Exceptions;
using Eparafia.Application.Services.Jwt;
using Eparafia.Application.Services.UserProvider;
using Eparafia.Infrastructure.Exceptions;
using MediatR;

namespace Eparafia.Application.Actions.PriestAuth.Query;

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