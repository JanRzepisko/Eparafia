using Eparafia.Application.DataAccess;
using Eparafia.Application.Services;
using Eparafia.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Intention.Command;

public static class AddIntention
{
    public sealed record Command
        (string Content, DateTime Date, IntentionType Type, bool AutomaticAllocation) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IIntentionService _intentionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider,
            IIntentionService intentionService)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
            _intentionService = intentionService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);
            var parishId = priest.Parish.Id;
            var resultDate = request.Date;

            if (request.AutomaticAllocation)
            {
                resultDate = await _intentionService.CalculateNextIntentionDateAsync(parishId, request.Content,
                    _unitOfWork, cancellationToken);
            }
            else
            {
                var oldIntentionOnDate =
                    await _unitOfWork.Intentions.GetByDateAsync(parishId, request.Date, cancellationToken);
                if (!oldIntentionOnDate.AutomaticAllocation)
                {
                    throw new InvalidRequestException("Date is already taken");
                }

                oldIntentionOnDate.Date = await _intentionService.CalculateNextIntentionDateAsync(parishId,
                    oldIntentionOnDate.Content, _unitOfWork, cancellationToken, 2);
                resultDate = request.Date;
            }

            var intention = new Domain.Entities.Intention
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

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Date > DateTime.Now);
            }
        }
    }
}