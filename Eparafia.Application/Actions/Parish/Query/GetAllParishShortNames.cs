using Eparafia.Application.DataAccess;
using Eparafia.Domain.DTOs;
using FluentValidation;
using MassTransit.Initializers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Parish.Query;

public static class GetAllParishShortNames
{
    public sealed record Query() : IRequest<List<Domain.Entities.Parish>>;

    public class Handler : IRequestHandler<Query, List<Domain.Entities.Parish>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<Domain.Entities.Parish>> Handle(Query request, CancellationToken cancellationToken)
        {
            return _unitOfWork.Parishes.GetAllAsync(cancellationToken);
        }

        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
    }
}