using Eparafia.Application.Actions.LiturgicalCalendar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;
using Shared.BaseModels.Jwt;

namespace Eparafia.API.Controllers;

[Route("LiturgicalCalendar")]
[ApiController]
public class LiturgicalCalendarController : BaseApiController
{
    public LiturgicalCalendarController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get() => await Endpoint(new GetToday.Query());
}