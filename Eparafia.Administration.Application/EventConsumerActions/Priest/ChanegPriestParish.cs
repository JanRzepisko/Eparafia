using Eparafia.Administration.Application.DataAccess;
using FluentValidation;
using MediatR;

namespace Eparafia.Administration.Application.EventConsumerActions.Priest;

public static class ChangePriestParish
{
    public sealed record Command(Guid PriestId, Guid? ParishId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(request.PriestId, cancellationToken);
            priest.ParishId = request.ParishId;
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