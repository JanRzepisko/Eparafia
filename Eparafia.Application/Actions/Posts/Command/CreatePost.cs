using Eparafia.Application.DataAccess;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using Eparafia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Posts.Command;

public static class CreatePost
{
    public sealed record Command(string Title, string Content, List<Base64File> Base64Files) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUserProvider _userProvider;
        private readonly IUnitOfWork _unitOfWork;
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
            Guid id = Guid.NewGuid();
            var post = new Post
            {
                Title = request.Title,
                ParishId = (Guid)priest.ParishId,
                Content = request.Content,
                AuthorId = priest.Id,
                Id = id,
                PublishDate = DateTime.Now,
                Files = new List<PostFile>()
            };

            foreach (var item in request.Base64Files)
            {
                Guid newId = Guid.NewGuid();
                var file = await _fileManager.SaveImageAsync(item.Base64, ImageType.PostPhoto, newId, cancellationToken);
                var newPostFile = new PostFile
                {
                    Id = newId,
                    PostId = id,
                    FilePath = file.Item1,
                    FilePathMin = file.Item2,
                    FileType = item.FileType
                };
                
                post.Files.Add(newPostFile);
            }
            
            await _unitOfWork.Posts.AddAsync(post, cancellationToken);
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