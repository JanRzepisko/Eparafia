using Eparafia.Application.DataAccess;
using FluentValidation;
using MediatR;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.Priest;

public static class GetPriestById
{
    public sealed record Query : IRequest<Domain.Entities.Priest>;

    public class Handler : IRequestHandler<Query, Domain.Entities.Priest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<Domain.Entities.Priest> Handle(Query request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id, cancellationToken);
            return priest;
        }

        public sealed class Validator : AbstractValidator<Query>
        {
        }
    }
}