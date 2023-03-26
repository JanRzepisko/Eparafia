using Eparafia.Application.DataAccess;
using Eparafia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Posts.Query;

public static class GetLatestPosts
{
    public sealed record Query(Guid ParishId, int Page) : IRequest<List<Post>>;

    public class Handler : IRequestHandler<Query, List<Post>>
    {
        private readonly int _pageSize;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _pageSize = configuration.GetValue<int>("PageSize");
        }

        public async Task<List<Post>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Posts.GetLatestPosts(request.ParishId, request.Page, _pageSize, cancellationToken);
        }

        public sealed class Validator : AbstractValidator<Query>
        {
        }
    }
}