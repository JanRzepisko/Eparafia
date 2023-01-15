using Eparafia.Application.DataAccess;
using Eparafia.Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class RemovePost
{
    public sealed record Command(Guid PostId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(request.PostId, cancellationToken);
            if (post is null) 
            {
                throw new InvalidRequestException("Post not found");
            }
            
            _unitOfWork.Posts.Remove(post);
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