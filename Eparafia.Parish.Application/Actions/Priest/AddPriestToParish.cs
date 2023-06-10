using Eparafia.Application.DataAccess;
using FluentValidation;
using MediatR;
using Shared.Messages;
using Shared.Service.Interfaces;
using Shared.Service.Interfaces.MessageBus;

namespace Eparafia.Application.Actions.Priest;

public static class AddPriestToParish
{
    public sealed record Command(Guid PriestId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;
        private readonly IMessageBusClient _messageBusClient;

        public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider, IMessageBusClient messageBusClient)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
            _messageBusClient = messageBusClient;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(request.PriestId, cancellationToken);

            var adderPriest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.UserId, cancellationToken);

            if (priest is null) throw new Exception("Priest not found");
            if (priest.ParishId != null) throw new Exception("Priest is already assigned to a parish");

            priest.ParishId = adderPriest.ParishId;
            _unitOfWork.SaveChangesAsync(cancellationToken);
            _messageBusClient.SendAsync(new ChangedParishPriestBusEvent
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