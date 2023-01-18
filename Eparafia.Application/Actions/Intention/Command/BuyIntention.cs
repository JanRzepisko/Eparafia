using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Enums;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class BuyIntention
{
    public sealed record Command(Guid ParishId, string Content, DateTime Date, IntentionType Type, bool AutomaticAllocation) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var parish = await _unitOfWork.Parishes.GetByIdAsync(request.ParishId, cancellationToken);
            if (parish is null)
            {
                throw new EntityNotFoundException("Parish not found");
            }
            
            DateTime date = request.Date;
            
            if (request.AutomaticAllocation)
            {
                //Automatic allocation system
                //var date = result
            }
            
            var intention = new Intention
            {
                Content = request.Content,
                Date = request.Date,
                ParishId = parish.Id,
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