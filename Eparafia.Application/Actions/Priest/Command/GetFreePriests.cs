using System.Data;
using Eparafia.Application.DataAccess;
using Eparafia.Application.DTOs.Rezluts;
using Eparafia.Application.Entities;
using Eparafia.Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class GetFreePriests
{
    public sealed record Query(string? query, int Page) : IRequest<List<PublicPriestProfile>>;

    public class Handler : IRequestHandler<Query, List<PublicPriestProfile>>
    {
        private readonly int _pageSize;

        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _pageSize = int.Parse(configuration["PageSize"]);
        }

        public async Task<List<PublicPriestProfile>?> Handle(Query request, CancellationToken cancellationToken)
        {
            string query = request.query ?? string.Empty;

            var priests = await _unitOfWork.Priests.GetFreePriestAsync(query, request.Page, _pageSize, cancellationToken);

            if (priests is null)
            {
                throw new InvalidRequestException("Not found result");
            }

            var result = new List<PublicPriestProfile>();
            foreach (var priest in priests)
            {
                result.Add(PublicPriestProfile.Create(priest));
            }
            
            return result;
        }

        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                
            }
        }
    }
}