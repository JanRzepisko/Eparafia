using Eparafia.Application.DataAccess;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.EventBus;
using Shared.Messages;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Priest;

public static class AddPriestToParish
{
    public sealed record Command(Guid PriestId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;
        private readonly IEventBus _eventBus;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(request.PriestId, cancellationToken);

            var adderPriest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);

            if (priest is null) throw new Exception("Priest not found");
            if (priest.ParishId != null) throw new Exception("Priest is already assigned to a parish");

            priest.ParishId = adderPriest.ParishId;
            _unitOfWork.SaveChangesAsync(cancellationToken);
            _eventBus.PublishAsync(new ChangedParishPriestBusEvent
            {
                ParishId = adderPriest.ParishId,
                PriestId = priest.Id,
            }, cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}