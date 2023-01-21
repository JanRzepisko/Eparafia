using Eparafia.Application.DataAccess;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.UserProvider;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Intention.Command;

public static class AddIntention
{
    public sealed record Command(string Content, DateTime Date, IntentionType Type, bool AutomaticAllocation) : IRequest<Unit>;

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
            var parishId = priest.Parish.Id;
            var resultDate = request.Date;

            if (request.AutomaticAllocation)
            {
                resultDate = await CalculateDate(parishId, request.Content, cancellationToken);
            }

            var intention = new Entities.Intention
            {
                Content = request.Content,
                Date = resultDate,
                ParishId = ((Guid)priest.ParishId)!,
                Type = request.Type,
                AutomaticAllocation = request.AutomaticAllocation
            };
            await _unitOfWork.Intentions.AddAsync(intention, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        //Automatic allocation system
        private async Task<DateTime> CalculateDate(Guid parishId, string content, CancellationToken cancellationToken)
        {
            var week = await _unitOfWork.CommonWeek.GetByParishId(parishId, cancellationToken);
            for (var i = 1; true; i++)
            {
                var existInDay = new bool[7];
                var startOfWeek =
                    DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)(DayOfWeek.Monday) + (i - 1) * 7);
                DateTime resultDate;
                for (var j = 0; i != 6; i++)
                {
                    var specialEventsForDay =
                        await _unitOfWork.SpecialEvents.GetForDay(parishId, startOfWeek.AddDays(j), cancellationToken);
                    if (specialEventsForDay is null)
                    {
                        break;
                    }
                    foreach (var item in specialEventsForDay)
                    {
                        var intentionInSpecial = await _unitOfWork.Intentions.GetByDate(parishId, item.Date, cancellationToken);
                        if (intentionInSpecial is null) continue;
                        var intentionInDayExist = await _unitOfWork.Intentions.ExistContentInDay(parishId, intentionInSpecial.Date, content, cancellationToken);
                        if (intentionInDayExist) continue;
                        resultDate = intentionInSpecial.Date;
                        return resultDate;
                    }
                }
                foreach (var @event in week)
                {
                    var intention = await _unitOfWork.Intentions.GetByDate(parishId,
                        startOfWeek.AddDays((int)@event.DayOfWeek).AddHours(@event.Time.Hours)
                            .AddMinutes(@event.Time.Minutes), cancellationToken);
                    if (intention is null && !await _unitOfWork.Intentions.ExistContentInDay(parishId,
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

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Date > DateTime.Now);
            }
        }
    }
}