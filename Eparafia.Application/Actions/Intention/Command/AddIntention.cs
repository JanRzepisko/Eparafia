using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.UserProvider;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Eparafia.Application.Actions.Parish;

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

            DateTime resultDate = request.Date;

            Intention? intention;
            if (request.AutomaticAllocation)
            {
                //Automatic allocation system
                var week = await _unitOfWork.CommonWeek.GetByParishId((Guid)priest.ParishId, cancellationToken);
                for (int i = 1; true; i++)
                {
                    bool[] existInDay = new bool[7];

                    bool exit = false;
                    var startOfWeek =
                        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)(DayOfWeek.Monday) +
                                               ((i - 1) * 7));

                    foreach (var @event in week)
                    {
                        intention = await _unitOfWork.Intentions.GetByDate((Guid)priest.ParishId, startOfWeek.AddDays((int)@event.DayOfWeek).AddHours(@event.Time.Hours).AddMinutes(@event.Time.Minutes), cancellationToken);
                        if (intention is null && !await _unitOfWork.Intentions.ExistContentInDay((Guid)priest.ParishId, startOfWeek.AddDays((int)@event.DayOfWeek), request.Content, cancellationToken))
                        {
                            DateTime date = startOfWeek.AddDays((int)@event.DayOfWeek).AddHours(@event.Time.Hours).AddMinutes(@event.Time.Minutes);
                            if (date > DateTime.Now)
                            {
                                resultDate = date;
                                exit = true;
                                break;
                            }
                        }
                        else
                        {
                            existInDay[(int)@event.DayOfWeek] = true;
                        }
                    }
                    if (exit)
                    {
                        break;
                    }
                }
            }

            intention = new Intention
            {
                Content = request.Content,
                Date = resultDate,
                ParishId = (Guid)priest.ParishId,
                Type = request.Type,
                AutomaticAllocation = request.AutomaticAllocation
            };
            await _unitOfWork.Intentions.AddAsync(intention, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
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