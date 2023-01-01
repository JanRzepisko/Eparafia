using System.Data;
using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Exceptions;
using Eparafia.Application.Services.UserProvider;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class AnnouncementsRemove
{
    public sealed record Command(Guid Id) : IRequest<Unit>;

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
            var announcement = await _unitOfWork.Announcements.GetByIdAsync(request.Id, cancellationToken);
            if (announcement is null)
            {
                throw new InvalidRequestException("Announcement not found");
            }
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