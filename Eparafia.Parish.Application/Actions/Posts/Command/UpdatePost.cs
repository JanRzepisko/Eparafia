using Eparafia.Application.DataAccess;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using Eparafia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;

namespace Eparafia.Application.Actions.Parish;

public static class UpdatePost
{
    public sealed record Command(Guid PostId, string? Title, string? Content, List<Base64File>? Files,
        List<Guid>? FilesToRemove) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IFileManager _fileManager;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(request.PostId, cancellationToken);
            if (post is null) throw new InvalidRequestException("Post not found");

            post.Title = request.Title ?? post.Title;
            post.Content = request.Content ?? post.Content;

            if (request.FilesToRemove is not null)
                foreach (var file in request.FilesToRemove)
                {
                    _fileManager.RemoveImage(ImageType.PostPhoto, file, cancellationToken);
                    _unitOfWork.PostFiles.RemoveById(file);
                }

            if (request.Files is not null)
                foreach (var file in request.Files)
                {
                    var filePaths = await _fileManager.SaveImageAsync(file.Base64, ImageType.PostPhoto, Guid.NewGuid(),
                        cancellationToken);
                    _unitOfWork.PostFiles.AddAsync(new PostFile
                    {
                        PostId = post.Id,
                        FilePath = filePaths.Item1,
                        FilePathMin = filePaths.Item2,
                        FileType = file.FileType
                    }, cancellationToken);
                }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}