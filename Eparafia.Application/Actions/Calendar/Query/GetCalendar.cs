using Eparafia.Application.DataAccess;
using Eparafia.Application.DTOs;
using Eparafia.Application.Entities;
using Eparafia.Application.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class GetCalendar
{
    public sealed record Query(Guid ParishId, int Week) : IRequest<List<SpecialEvent>>;

    public class Handler : IRequestHandler<Query, List<SpecialEvent>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SpecialEvent>> Handle(Query request, CancellationToken cancellationToken)
        {
            var commonWeek = await _unitOfWork.CommonWeek.GetByParishId(request.ParishId, cancellationToken);
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)(DayOfWeek.Monday) + ((request.Week) * 7));
            var specialEvents = await _unitOfWork.SpecialEvents.GetForWeek(request.ParishId, startOfWeek, cancellationToken);

            var calendar = commonWeek.Select(c => new SpecialEvent()
            {
                ParishId = c.ParishId,
                Date = startOfWeek.AddDays((int)c.DayOfWeek).AddHours(c.Time.Hours).AddMinutes(c.Time.Minutes),
                Event = new Event()
                {
                    Description = c.Event.Description,
                    Name = c.Event.Name,
                    Type = c.Event.Type,
                },
            }).ToList();

            foreach (var specialEvent in specialEvents)
            {
                calendar.Add(specialEvent);
            }

            foreach (var @event in calendar)
            {
                @event.Intention =
                    await _unitOfWork.Intentions.GetByDateAsync(@event.ParishId, @event.Date, cancellationToken);

            }
            
            return calendar.OrderBy(c => c.Date).ToList();
        }

        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
    }
}