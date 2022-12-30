using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using Eparafia.Application.Services.UserProvider;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.PriestAuth.Command;

public static class UpdatePriest
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
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);

            if (priest is null)
            {
                throw new  EntityNotFoundException($"user with id {_userProvider.Id} not found");
            }
            priest.Name = request.Name ?? priest.Name;
            priest.Surname = request.Surname ?? priest.Surname;
            priest.Email = request.Email ?? priest.Email;

            if (request.RemovePhoto)
            {
                _fileManager.RemoveImage(ImageType.PriestAvatar, _userProvider.Id, cancellationToken);
                priest.HasAvatar = false;
            }
            
            if(request.Base64 is not null)
            {
                _fileManager.RemoveImage(ImageType.PriestAvatar, _userProvider.Id, cancellationToken);
                await _fileManager.SaveImageAsync(request.Base64,ImageType.UserAvatar, _userProvider.Id, cancellationToken);
                priest.HasAvatar = true;
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