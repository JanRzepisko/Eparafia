using Eparafia.Application.DataAccess;
using Eparafia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Calendar.Query;

public static class GetCommonWeek
{
    public sealed record Query(Guid ParishId) : IRequest<List<CommonEvent>>;

    public class Handler : IRequestHandler<Query, List<CommonEvent>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CommonEvent>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CommonWeek.GetByParishId(request.ParishId, cancellationToken);
            return result;
        }

        public sealed class Validator : AbstractValidator<Query>
        {
        }
    }
}