using System.Security.Cryptography;
using Eparafia.Bible.Application.DataAccess;
using Eparafia.Bible.Domain.Entities;
using Eparafia.Bible.Domain.Enums;
using FluentValidation;
using MediatR;

namespace Eparafia.Bible.Application.Actions.Query;

public static class Get
{
    public sealed record Command() : IRequest<List<Reslut>>;

    public class Reslut
    {
        public string Name { get; set; }
        public int ReadingsCount { get; set; }
        public int YearType { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, List<Reslut>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Reslut>> Handle(Command request, CancellationToken cancellationToken)
        {
            var days = await _unitOfWork.Days.GetAllAsync(cancellationToken);

            var result = days.Select(x => new Reslut
            {
                Name = x.Name,
                YearType = (int)x.Year
            }).ToList();

            return result;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {

            }
        }
    }
}