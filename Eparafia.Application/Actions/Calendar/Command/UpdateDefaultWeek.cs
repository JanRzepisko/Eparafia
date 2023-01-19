using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.UserProvider;
using Eparafia.Application.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Calendar.Command;

public static class UpdateDefaultWeek
{
    public sealed record Command(List<CommonEventRequestObject> CommonWeek) : IRequest<Unit>;

    public class CommonEventRequestObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Duration { get; set; }
        public DayOfWeek Day { get; set; }
    }
    
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
            var commonEvents = await _unitOfWork.CommonWeek.GetByParishId((Guid)priest.ParishId, cancellationToken);
            
            foreach (var commonEvent in commonEvents)
            {
                _unitOfWork.CommonWeek.Remove(commonEvent);
            }
            
            var commonWeek = request.CommonWeek.OrderBy(c => c.Day).ThenBy(c => c.Hour).ThenBy(c => c.Minute).ToList();
            int indexOfEvent = 0;
            foreach (var commonEvent in commonWeek)
            {
                await _unitOfWork.CommonWeek.AddAsync(new Entities.CommonEvent()
                {
                    DayOfWeek = commonEvent.Day,
                    Time = new TimeSpan(commonEvent.Hour, commonEvent.Minute, 0),
                    Event = new Event()
                    {
                        Name = commonEvent.Name,
                        Description = commonEvent.Description,
                        Type = commonEvent.Type,
                        Duration = commonEvent.Duration,
                    },
                    EventInWeekId = indexOfEvent,
                    ParishId = (Guid)priest.ParishId,
                    Intention = null,
                    IntentionId = null
                }, cancellationToken);
                
                indexOfEvent++;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {

            }
        }
    }
}

/*
{
  "commonWeek": [
    {
      "name": "Msza św 6 30",
      "description": "Normalna msza",
      "type": 0,
      "hour": 6,
      "minute": 30,
      "day": 0
    },    
{
      "name": "Msza św 6 30",
      "description": "Normalna msza",
      "type": 0,
      "hour": 6,
      "minute": 30,
      "day": 1
    },
{
      "name": "Msza św 6 30",
      "description": "Normalna msza",
      "type": 0,
      "hour": 6,
      "minute": 30,
      "day": 2
    },
{
      "name": "Msza św 6 30",
      "description": "Normalna msza",
      "type": 0,
      "hour": 6,
      "minute": 30,
      "day": 3
    },
{
      "name": "Msza św 6 30",
      "description": "Normalna msza",
      "type": 0,
      "hour": 6,
      "minute": 30,
      "day": 4
    },
{
      "name": "Msza św 6 30",
      "description": "Normalna msza",
      "type": 0,
      "hour": 6,
      "minute": 30,
      "day": 5
    },
{
      "name": "Msza św 6 30",
      "description": "Niedzielna msza",
      "type": 0,
      "hour": 6,
      "minute": 30,
      "day": 6
    },
{
      "name": "Msza św 6 30",
      "description": "Normalna msza",
      "type": 0,
      "hour": 6,
      "minute": 30,
      "day": 1
    },
{
      "name": "Msza św 6 30",
      "description": "Normalna msza",
      "type": 0,
      "hour": 6,
      "minute": 30,
      "day": 1
    },
{
      "name": "Msza św 6 30",
      "description": "Normalna msza",
      "type": 0,
      "hour": 6,
      "minute": 30,
      "day": 1
    },
{      "name": "Msza św 18 30",
      "description": "Niedzielna msza",
      "type": 0,
      "hour": 18,
      "minute": 30,
      "day": 6
    }
  ]
}
*/