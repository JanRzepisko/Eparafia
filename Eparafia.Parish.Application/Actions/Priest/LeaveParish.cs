using Eparafia.Application.DataAccess;
using FluentValidation;
using MediatR;
using Shared.Messages;
using Shared.Service.Interfaces;
using Shared.Service.Interfaces.MessageBus;

namespace Eparafia.Application.EventConsumerActions.Priest.Command;

public static class LeaveParish
{
    public sealed record Command : IRequest<Unit>;

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
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);

            priest.ParishId = null;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _messageBusClient.SendAsync(new ChangedParishPriestBusEvent
            {
                ParishId = null,
                PriestId = priest.Id,
            }, cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}