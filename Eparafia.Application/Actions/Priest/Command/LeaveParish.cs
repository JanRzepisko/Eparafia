using Eparafia.Application.DataAccess;
using Eparafia.Application.Services.UserProvider;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class LeftFromParish
{
    public sealed record Command() : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;
        
        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);

            priest.ParishId = null;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {

            }
        }
    }
}