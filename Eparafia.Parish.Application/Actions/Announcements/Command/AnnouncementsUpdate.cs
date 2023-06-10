using Eparafia.Application.DataAccess;
using Eparafia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Announcements.Command;

public static class AnnouncementsUpdate
{
    public sealed record Command
        (List<string>? Records, string? Title, DateTime? Date, Guid Id) : IRequest<Unit>;

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
            if (announcements == null)
            {
                throw new EntityNotFoundException("Announcement not found");
            }

            foreach (var item in announcements.AnnouncementsRecords)
            {
                _unitOfWork.AnnouncementsRecords.Remove(item);
            }

            foreach (var item in request.Records)
            {
                var record = new AnnouncementRecord()
                {
                    AnnouncementId = announcements.Id,
                    Content = item
                };
                await _unitOfWork.AnnouncementsRecords.AddAsync(record, cancellationToken);
            }
            
            announcements.Title = request.Title ?? announcements.Title;
            announcements.PublishDate = request.Date ?? announcements.PublishDate;
            announcements.AuthorId = _userProvider.UserId;

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