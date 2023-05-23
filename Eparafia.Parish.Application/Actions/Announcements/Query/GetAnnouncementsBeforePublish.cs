using Eparafia.Application.DataAccess;
using Eparafia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Parish;

public static class GetAnnouncementsBeforePublish
{
    public sealed record Query(int Page) : IRequest<List<Announcement>>;

    public class Handler : IRequestHandler<Query, List<Announcement>>
    {
        private readonly int _pageSize;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
            _pageSize = configuration.GetValue<int>("PageSize");
        }

        public async Task<List<Announcement>> Handle(Query request, CancellationToken cancellationToken)
        {
            
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);
            return await _unitOfWork.Announcements.GetAnnouncementsBeforePublish((Guid)priest.ParishId, request.Page, _pageSize, cancellationToken);
        }

        public sealed class Validator : AbstractValidator<Query>
        {
        }
    }
}