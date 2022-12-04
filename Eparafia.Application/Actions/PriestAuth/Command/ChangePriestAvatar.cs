using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using Eparafia.Application.Services.UserProvider;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;

namespace Eparafia.Application.Actions.PriestAuth.Command;

public static class ChangePriestAvatar
{
    public sealed record Command(string Base64) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;        
        private readonly IUserProvider _userProvider;
        private readonly IFileManager _fileManager;

        public Handler(IUnitOfWork unitOfWork, IFileManager fileManager, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _userProvider = userProvider;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);
            if (priest == null)
            {
                throw new EntityNotFoundException(nameof(User), _userProvider.Id);
            }

            await _fileManager.SaveImageAsync(request.Base64, ImageType.PriestAvatar, priest.Id, cancellationToken);
            
            priest.HasAvatar = true;
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