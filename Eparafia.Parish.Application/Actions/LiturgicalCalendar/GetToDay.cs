using System.Globalization;
using Eparafia.Application.DataAccess;
using Eparafia.Domain.Objects.ResponseModel;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace Eparafia.Application.Actions.LiturgicalCalendar;

public static class GetToday
{
    public sealed record Query() : IRequest<LiturgicalCalendarDTO>;

    public class Handler : IRequestHandler<Query, LiturgicalCalendarDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LiturgicalCalendarDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            var response =
                await client.GetAsync(
                    $"http://calapi.inadiutorium.cz/api/v0/en/calendars/default/{DateTime.Today.Year}/{DateTime.Today.Month}/{DateTime.Today.Day}",
                    cancellationToken);

            var colours = new List<string>();
            var ranks = new List<string>();
            var seasons = new List<string>();


            var content =
                JsonConvert.DeserializeObject<LiturgicalCalendarResponseModel>(
                    await response.Content.ReadAsStringAsync(cancellationToken));

            return LiturgicalCalendarDTO.FromModel(content);
        }

        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
    }
}

/*
[0] = {string} "green"
[1] = {string} "white"
[2] = {string} "red"
[3] = {string} "violet"

[0] = {string} "Sunday"
[1] = {string} "ferial"
[2] = {string} "memorial"
[3] = {string} "solemnity"
[4] = {string} "optional memorial"
[5] = {string} "feast"
[6] = {string} "Primary liturgical days"
[7] = {string} "commemoration"
[8] = {string} "Easter triduum"

[0] = {string} "ordinary"
[1] = {string} "advent"
[2] = {string} "christmas"
[3] = {string} "lent"
[4] = {string} "easter"
*/