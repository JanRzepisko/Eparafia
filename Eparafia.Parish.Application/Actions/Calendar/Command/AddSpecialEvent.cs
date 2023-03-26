using Eparafia.Application.DataAccess;
using Eparafia.Domain.Entities;
using Eparafia.Domain.Enums;
using Eparafia.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Calendar.Command;

public static class AddSpecialEvent
{
    public sealed record Command
        (string Name, string Description, EventType Type, int Duration, DateTime Date) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);
            var newEvent = new SpecialEvent
            {
                Date = request.Date,
                Event = new Event
                {
                    Description = request.Description,
                    Type = request.Type,
                    Name = request.Name,
                    Duration = request.Duration
                },
                ParishId = (Guid)priest.ParishId,
                Intention = null
            };

            await _unitOfWork.SpecialEvents.AddAsync(newEvent, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}