using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.UserProvider;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

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

            DateTime date = request.Date;
            
            if (request.AutomaticAllocation)
            {
                //Automatic allocation system
                var week = await _unitOfWork.CommonWeek.GetByParishId(date, cancellationToken);
                
                //var date = result
            }
            
            var intention = new Intention
            {
                Content = request.Content,
                Date = request.Date,
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