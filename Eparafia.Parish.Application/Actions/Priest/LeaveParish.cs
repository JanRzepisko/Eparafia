using Eparafia.Application.DataAccess;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.EventBus;
using Shared.Messages;
using Shared.Service.Interfaces;

namespace Eparafia.Application.EventConsumerActions.Priest.Command;

public static class LeaveParish
{
    public sealed record Command : IRequest<Unit>;

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
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);

            priest.ParishId = null;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _eventBus.PublishAsync(new ChangeParishPriest
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