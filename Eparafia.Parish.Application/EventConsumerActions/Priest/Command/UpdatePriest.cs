using Eparafia.Application.DataAccess;
using Eparafia.Application.Services.FileManager;
using FluentValidation;
using MediatR;

namespace Eparafia.Application.EventConsumerActions.Priest.Command;

public static class UpdatePriest
{
    public sealed record Command(Guid PriestId, string? Name, string PhotoPath, string PhotoPathMin) : IRequest<Unit>;

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

            priest.Name = request.Name;
            priest.PhotoPath = new PhotoPath(request.PhotoPath, request.PhotoPathMin);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}