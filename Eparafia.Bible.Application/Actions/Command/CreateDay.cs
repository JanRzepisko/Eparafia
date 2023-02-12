using Eparafia.Bible.Application.DataAccess;
using Eparafia.Bible.Domain.Entities;
using Eparafia.Bible.Domain.Enums;
using FluentValidation;
using MediatR;

namespace Eparafia.Bible.Application.Actions.Command;

public static class CreateDay
{
    public sealed record Command(string DayName, YearType YearType, List<ReadingObject> Readings) : IRequest<Unit>;

    public class ReadingObject
    {
        public ReadingType Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            Guid id = Guid.NewGuid();
            var day = new Day
            {
                Id = id,
                Name = request.DayName,
                Year = request.YearType,
                Readings = request.Readings.Select(x => new Reading
                {
                    Type = x.Type,
                    Content = x.Content,
                    DayId = id,
                    Title = x.Title
                }).ToList()
            };
            
            await _unitOfWork.Days.AddAsync(day, cancellationToken);
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