using Eparafia.Application.DataAccess;
using Eparafia.Application.DTOs;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

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