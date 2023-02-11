using Eparafia.Application.DataAccess;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Parish.Command;

public static class AddPriestToParish
{
    public sealed record Command(Guid PriestId) : IRequest<Unit>;

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
            var priest = await _unitOfWork.Priests.GetByIdAsync(request.PriestId, cancellationToken);

            var adderPriest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);
            
            if (priest is null)
            {
                throw new Exception("Priest not found");
            }
            if(priest.ParishId != null)
            {
                throw new Exception("Priest is already assigned to a parish");
            }
            
            priest.ParishId = adderPriest.ParishId;
            _unitOfWork.SaveChangesAsync(cancellationToken);
            
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