using Eparafia.API.Models;
using Eparafia.Application.Actions.Parish;
using Eparafia.Application.Actions.PriestAuth.Command;
using Eparafia.Application.Actions.PriestAuth.Query;
using Eparafia.Application.Actions.UserAuth.Command;
using Eparafia.Application.Actions.UserAuth.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eparafia.API.Controllers;

[ApiController]
[Route("Priest")]
public class PriestController : Controller
{
    private readonly IMediator _mediator;

    public PriestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> LoginPriest(LoginPriest.Query query, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterPriest(RegisterPriest.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("Update")]
    public async Task<IActionResult> UpdatePriest(UpdatePriest.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpDelete("Remove")]
    public async Task<IActionResult> RemovePriest(RemovePriest.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }        
    [Authorize]
    [HttpDelete("LeaveParish")]
    public async Task<IActionResult> LeaveParish(LeaveParish.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    
    [Authorize]
    [HttpGet("FreePriests")]
    public async Task<IActionResult> GetFreePriests(string? query, int page, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetFreePriests.Query(query, page), cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }
}