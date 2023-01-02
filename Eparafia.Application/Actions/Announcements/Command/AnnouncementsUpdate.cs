using System.Data;
using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Services.UserProvider;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class AnnouncementsUpdate
{
    public sealed record Command(List<AnnouncementsRecords>? Records, string? Title, DateTime? Date, Guid Id) : IRequest<Unit>;

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
            var announcements = await _unitOfWork.Announcements.GetByIdAsync(request.Id, cancellationToken);

            announcements.AnnouncementsRecords = request.Records.Select(c => new AnnouncementsRecords
            {
                Announcement = announcements,
                Content = c.Content,
                Id = c.Id,
                AnnouncementId = announcements.Id
            }).ToList();
            announcements.Title = request.Title ?? announcements.Title;
            announcements.Date = request.Date ?? announcements.Date;
            announcements.AuthorId = _userProvider.Id;
            
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