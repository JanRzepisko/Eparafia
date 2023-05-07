using Eparafia.Administration.Application.DataAccess;
using Eparafia.Application.Services.FileManager;
using Eparafia.Domain.Enums;
using FluentValidation;
using MediatR;

namespace Eparafia.Administration.Application.EventConsumerActions.Priest;

public static class CreatePriest
{
    public sealed record Command(Guid Id, string Name) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var newPriest = new Eparafia.Administration.Domain.Entities.Priest
            {
                Id = request.Id,
                Name = request.Name,
                ParishId = null,  
            };

            await _unitOfWork.Priests.AddAsync(newPriest, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}