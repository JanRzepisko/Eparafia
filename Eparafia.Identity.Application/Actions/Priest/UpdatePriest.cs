using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Shared.BaseModels.Exceptions;
using Shared.EventBus;
using Shared.Messages;
using Shared.Service.Interfaces;

namespace Eparafia.Identity.Application.Actions.Priest;

public static class UpdatePriest
{
    public sealed record Command(string? Name, string? Surname, string? Email, string? Base64, bool? RemovePhoto,
        Contact? Contact) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IEventBus _eventBus;
        private readonly IFileManager _fileManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider, IFileManager fileManager, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
            _fileManager = fileManager;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);

            if (priest is null) throw new EntityNotFoundException($"user with id {_userProvider.Id} not found");
            priest.Name = request.Name ?? priest.Name;
            priest.Surname = request.Surname ?? priest.Surname;
            priest.Email = request.Email ?? priest.Email;
            priest.Contact = request.Contact ?? priest.Contact;


            if (request.RemovePhoto ?? false)
            {
                _fileManager.RemoveImage(ImageType.PriestAvatar, _userProvider.Id, cancellationToken);
                priest.PhotoPath = string.Empty;
                priest.PhotoPathMin = string.Empty;
            }

            if (request.Base64 is not null)
            {
                _fileManager.RemoveImage(ImageType.PriestAvatar, _userProvider.Id, cancellationToken);
                var paths = await _fileManager.SaveImageAsync(request.Base64, ImageType.UserAvatar, _userProvider.Id,
                    cancellationToken);
                priest.PhotoPath = paths.Item1;
                priest.PhotoPathMin = paths.Item2;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _eventBus.PublishAsync(new PriestUpdatedBusEvent
            {
                PriestId = _userProvider.Id,
                Name = priest.Name + " " + priest.Surname,
                PhotoPath = priest.PhotoPath,
                PhotoPathMin = priest.PhotoPathMin
            }, cancellationToken);

            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}