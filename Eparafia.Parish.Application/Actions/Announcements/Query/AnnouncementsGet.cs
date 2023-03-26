using Eparafia.Application.DataAccess;
using Eparafia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class AnnouncementsGet
{
    public sealed record Query(Guid ParishId, int Page) : IRequest<List<Announcement>>;

    public class Handler : IRequestHandler<Query, List<Announcement>>
    {
        private readonly int _pageSize;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _pageSize = configuration.GetValue<int>("PageSize");
        }

        public Task<List<Announcement>> Handle(Query request, CancellationToken cancellationToken)
        {
            return _unitOfWork.Announcements.GetLatestAnnouncements(request.ParishId, request.Page, _pageSize,
                cancellationToken);
        }

        public sealed class Validator : AbstractValidator<Query>
        {
        }
    }
}