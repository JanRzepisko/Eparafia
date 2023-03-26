using Eparafia.Application.DataAccess;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;

namespace Eparafia.Application.Actions.Parish.Command;

public static class RemoveParish
{
    public sealed record Command(Guid ParishId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Parishes.ExistsAsync(request.ParishId, cancellationToken))
                throw new EntityNotFoundException("Parish not found");

            var parish = await _unitOfWork.Parishes.GetByIdAsync(request.ParishId, cancellationToken);

            foreach (var priest in parish.Priests) priest.ParishId = null;

            _unitOfWork.Parishes.Remove(parish);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}