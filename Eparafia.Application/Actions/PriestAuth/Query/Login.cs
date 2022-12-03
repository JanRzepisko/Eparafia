using Eparafia.Application.DataAccess;
using Eparafia.Application.Services.Jwt;
using Eparafia.Infrastructure.Exceptions;
using MediatR;

namespace Eparafia.Application.Actions.PriestAuth.Query;

public static class LoginPriest
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
            var priest = await _unitOfWork.Priests.GetByLoginAsync(request.Email, cancellationToken);
            if (priest is null)
            {
                throw new EntityNotFoundException("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, priest.PasswordHash))
            {
                throw new BadPassword($"Bad password");
            }

            return await _jwtAuth.GenerateJwt(priest);
        }
    }
}