using Eparafia.Application.DataAccess;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.User.Command;

public static class UpdateUser
{
    public sealed record Command(string? Name, string? Surname, string? Email, string? Base64, bool RemovePhoto) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;
        private readonly IFileManager _fileManager;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
            _fileManager = fileManager;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(_userProvider.Id, cancellationToken);
            if (user is null)
            {
                throw new  EntityNotFoundException($"user with id {_userProvider.Id} not found");
            }
            user.Name = request.Name ?? user.Name;
            user.Surname = request.Surname ?? user.Surname;
            user.Email = request.Email ?? user.Email;

            if (request.RemovePhoto)
            {
                _fileManager.RemoveImage(ImageType.UserAvatar, _userProvider.Id, cancellationToken);
                user.PhotoPath = String.Empty;
                user.PhotoPathMin = string.Empty;
            }
            if(request.Base64 is not null)
            {
                _fileManager.RemoveImage(ImageType.UserAvatar, _userProvider.Id, cancellationToken);
                var paths = await _fileManager.SaveImageAsync(request.Base64,ImageType.UserAvatar, _userProvider.Id, cancellationToken);
                user.PhotoPath = paths.Item1;
                user.PhotoPathMin = paths.Item2;
            }
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
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