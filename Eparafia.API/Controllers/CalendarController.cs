using Eparafia.API.Models;
using Eparafia.Application.Actions.Calendar.Command;
using Eparafia.Application.Actions.Calendar.Query;
using Eparafia.Application.Actions.Parish;
using Eparafia.Application.DTOs;
using Eparafia.Application.Services.Jwt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eparafia.API.Controllers;

[ApiController]
[Authorize(JwtPolicies.Priest)]
[Produces("application/json")]
[Route("Calendar")]
public class CalendarController : Controller
{
    private readonly IMediator _mediator;

    public CalendarController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut]
    public async Task<IActionResult> EditCommonWeek(UpdateDefaultWeek.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }

    [HttpGet]
    public async Task<IActionResult> GetCalendar(Guid parishId, int week, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetCalendar.Query(parishId, week), cancellationToken);
        return Ok(ApiResponse.Success(200, result.Select(EventDTO.FromEntity)));
    }
    [HttpGet]
    [Route("CommonWeek")]
    public async Task<IActionResult> GetCommonWeek(Guid parishId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetCommonWeek.Query(parishId), cancellationToken);
        return Ok(ApiResponse.Success(200, result.Select(CommonEventDTO.FromEntity)));
    }    

}