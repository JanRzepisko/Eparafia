using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using Eparafia.Identity.Application.DataAccess;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;
using Shared.Service.Interfaces;

namespace Eparafia.Identity.Application.Actions.User;

public static class UpdateUser
{
    public sealed record Command
        (string? Name, string? Surname, string? Email, string? Base64, bool RemovePhoto) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IFileManager _fileManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider,
            IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
            _fileManager = fileManager;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(_userProvider.UserId, cancellationToken);
            if (user is null) throw new EntityNotFoundException($"user with id {_userProvider.UserId} not found");
            user.Name = request.Name ?? user.Name;
            user.Surname = request.Surname ?? user.Surname;
            user.Email = request.Email ?? user.Email;

            if (request.RemovePhoto)
            {
                _fileManager.RemoveImage(ImageType.UserAvatar, _userProvider.UserId, cancellationToken);
                user.PhotoPath = string.Empty;
                user.PhotoPathMin = string.Empty;
            }

            if (request.Base64 is not null)
            {
                _fileManager.RemoveImage(ImageType.UserAvatar, _userProvider.UserId, cancellationToken);
                var paths = await _fileManager.SaveImageAsync(request.Base64, ImageType.UserAvatar, _userProvider.UserId,
                    cancellationToken);
                user.PhotoPath = paths.Item1;
                user.PhotoPathMin = paths.Item2;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}