using Eparafia.Application.DataAccess;
using Eparafia.Domain.DTOs;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;

namespace Eparafia.Application.Actions.Parish.Query;

public static class GetParishById
{
    public sealed record Query(Guid ParishId) : IRequest<ParishDTO>;

    public class Handler : IRequestHandler<Query, ParishDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ParishDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            var parish = await _unitOfWork.Parishes.GetByIdAsync(request.ParishId, cancellationToken);
            if (parish is null)
            {
                throw new EntityNotFoundException("Parafia", request.ParishId);
            }
            return ParishDTO.FromEntity(parish);
        }

        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
    }
}