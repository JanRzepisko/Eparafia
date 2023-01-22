using Eparafia.Application.DataAccess;
using Eparafia.Application.Services;

namespace Eparafia.Infrastructure.Services;

public class IntentionService : IIntentionService

{
    public async Task<DateTime> CalculateNextIntentionDateAsync(Guid parishId, string content, IUnitOfWork unitOfWork, CancellationToken cancellationToken, int startWeek = 0)
    {
        var week = await unitOfWork.CommonWeek.GetByParishId(parishId, cancellationToken);
        for (var i = 1; true; i++)
        {
            var existInDay = new bool[7];
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)(DayOfWeek.Monday) + (i + startWeek - 1) * 7);
            DateTime resultDate;

            var specialEventForWeek =
                await unitOfWork.SpecialEvents.GetForWeek(parishId, startOfWeek, cancellationToken);
            foreach (var @event in specialEventForWeek)
            {
                var existInDaySpecialEvent =
                    await unitOfWork.Intentions.ExistContentInDay(parishId, @event.Date, content, cancellationToken);
                if (!existInDaySpecialEvent)
                {
                    var exist = await unitOfWork.Intentions.GetByDateAsync(parishId, @event.Date, cancellationToken);
                    if (exist is null)
                    {
                        resultDate = @event.Date;
                        return resultDate;
                    }
                }
            }

            foreach (var @event in week)
            {
                var intention = await unitOfWork.Intentions.GetByDateAsync(parishId,
                    startOfWeek.AddDays((int)@event.DayOfWeek).AddHours(@event.Time.Hours)
                        .AddMinutes(@event.Time.Minutes), cancellationToken);
                if (intention is null && !await unitOfWork.Intentions.ExistContentInDay(parishId,
                        startOfWeek.AddDays((int)@event.DayOfWeek),
                        content, cancellationToken))
                {
                    DateTime date = startOfWeek.AddDays((int)@event.DayOfWeek).AddHours(@event.Time.Hours)
                        .AddMinutes(@event.Time.Minutes);
                    if (date > DateTime.Now)
                    {
                        resultDate = date;
                        return resultDate;
                    }
                }
                else
                {
                    existInDay[(int)@event.DayOfWeek] = true;
                }
            }
        }
    }
}