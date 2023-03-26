using Eparafia.Application.DataAccess;
using Eparafia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class SearchInAnnouncements
{
    public sealed record Query(Guid ParishId, string? query, int Page) : IRequest<List<AnnouncementRecord>>;

    public class Handler : IRequestHandler<Query, List<AnnouncementRecord>>
    {
        private readonly int _pageSize;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _pageSize = configuration.GetValue<int>("PageSize");
        }

        public Task<List<AnnouncementRecord>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = request.query ?? string.Empty;
            return _unitOfWork.AnnouncementsRecords.SearchInAnnouncements(request.ParishId, query, request.Page,
                _pageSize, cancellationToken);
        }

        public sealed class Validator : AbstractValidator<Query>
        {
        }
    }
}