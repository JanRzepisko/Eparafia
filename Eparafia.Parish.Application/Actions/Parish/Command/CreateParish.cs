using Eparafia.Application.DataAccess;
using Eparafia.Domain.Enums;
using Eparafia.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.EventBus;
using Shared.Messages;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Parish.Command;

public static class CreateParish
{
    public sealed record Command(string CallName, Address Address, Contact Contact) : IRequest<Unit>;

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

            var id = Guid.NewGuid();

            var parish = new Domain.Entities.Parish
            {
                CallName = request.CallName,
                ShortName = request.CallName.ToLower().Trim(' ') + request.Address.City.ToLower().Trim(' '),
                Contact = request.Contact,
                Priests = new List<Domain.Entities.Priest> { priest },
                Address = request.Address,
                Id = id
            };

            priest.ParishId = id;
            priest.FunctionParish = FunctionParish.Owner;
            await _unitOfWork.Parishes.AddAsync(parish, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _eventBus.PublishAsync(new ChangedParishPriestBusEvent()
            {
                ParishId = id,
                PriestId = priest.Id
            }, cancellationToken);

            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.CallName).MinimumLength(3).MaximumLength(50);
            }
        }
    }
}