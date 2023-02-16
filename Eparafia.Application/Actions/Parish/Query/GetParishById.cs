using Eparafia.Application.DataAccess;
using Eparafia.Domain.DTOs;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Parish.Query;

public static class GetParishById
{
    public sealed record Query() : IRequest<ParishDTO>;

    public class Handler : IRequestHandler<Query, ParishDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<ParishDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);
            var parish = await _unitOfWork.Parishes.GetByIdAsync(priest.ParishId, cancellationToken);
            if (parish is null)
            {
                throw new EntityNotFoundException("Parish Not Found");
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