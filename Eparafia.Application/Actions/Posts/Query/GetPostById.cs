using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class GetPostById
{
    public sealed record Query(Guid PostId) : IRequest<Post>;

    public class Handler : IRequestHandler<Query, Post>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Posts.GetByIdAsync(request.PostId, cancellationToken);
        }

        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
    }
}