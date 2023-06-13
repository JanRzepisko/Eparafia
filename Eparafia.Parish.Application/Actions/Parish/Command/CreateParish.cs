using Eparafia.Application.DataAccess;
using Eparafia.Domain.Enums;
using Eparafia.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Shared.Messages;
using Shared.Service.Interfaces;
using Shared.Service.Interfaces.MessageBus;

namespace Eparafia.Application.Actions.Parish.Command;

public static class CreateParish
{
    public sealed record Command(string CallName, Address Address, Contact Contact) : IRequest<Unit>;

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

            var id = Guid.NewGuid();

            var parish = new Domain.Entities.Parish
            {
                CallName = request.CallName,
                ShortName = NormalizeString(request.CallName.ToLower().Trim(' ') + request.Address.City.ToLower().Trim(' ')),
                Contact = request.Contact,
                Priests = new List<Domain.Entities.Priest> { priest },
                Address = request.Address,
                
                Id = id
            };

            priest.ParishId = id;
            priest.FunctionParish = FunctionParish.Owner;
            await _unitOfWork.Parishes.AddAsync(parish, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _messageBusClient.SendAsync(new ChangedParishPriestBusEvent
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

        private static string NormalizeString(string c)
        {
            return c.Replace("ą", "a")
                .Replace("ć", "c")
                .Replace("ę", "e")
                .Replace("ł", "l")
                .Replace("ń", "n")
                .Replace("ó", "o")
                .Replace("ś", "s")
                .Replace("ź", "z")
                .Replace("ż", "z");
        }

    }
}