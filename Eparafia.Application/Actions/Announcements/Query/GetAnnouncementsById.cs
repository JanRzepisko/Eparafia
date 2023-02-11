using Eparafia.Application.DataAccess;
using Eparafia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Announcements.Query;

public static class GetAnnouncementsById
{
    public sealed record Query(Guid AnnouncementId) : IRequest<Announcement>;

    public class Handler : IRequestHandler<Query, Announcement>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Announcement> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Announcements.GetByIdAsync(request.AnnouncementId, cancellationToken);
        }

        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
    }
}