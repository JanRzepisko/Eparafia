using Eparafia.Application.DataAccess;
using Eparafia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Announcements.Command;

public static class AnnouncementsCreate
{
    public sealed record Command(List<string> Records, string Title, DateTime Date) : IRequest<Unit>;

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
            var author = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);

            var id = Guid.NewGuid();
            var newAnnouncements = new Announcement
            {
                Id = id,
                AuthorId = _userProvider.Id,
                PublishDate = request.Date,
                ParishId = author.Parish.Id,
                Title = request.Title,
                AnnouncementsRecords = request.Records.Select(c => new AnnouncementRecord
                {
                    AnnouncementId = id,
                    Content = c
                }).ToList()
            };

            await _unitOfWork.Announcements.AddAsync(newAnnouncements, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Date > DateTime.Today.AddDays(-1));
                RuleFor(c => c.Records.Count > 0);
                RuleFor(c => c.Title).MinimumLength(5).MaximumLength(45);
            }
        }
    }
}