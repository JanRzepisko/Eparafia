using Eparafia.API.Models;
using Eparafia.Application.Actions.Parish;
using Eparafia.Application.Actions.PriestAuth.Command;
using Eparafia.Application.Actions.PriestAuth.Query;
using Eparafia.Application.Actions.UserAuth.Command;
using Eparafia.Application.Actions.UserAuth.Query;
using Eparafia.Application.Services.Jwt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eparafia.API.Controllers;

[ApiController]
[Authorize(JwtPolicies.Priest)]
[Produces("application/json")]
[Route("Parish")]
public class ParishController : Controller
{
    private readonly IMediator _mediator;

    public ParishController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Parish")]
    public async Task<IActionResult> CreateParish(CreateParish.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [HttpDelete("Parish")]
    public async Task<IActionResult> RemoveParish(RemoveParish.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    
    [HttpPut("Parish")]
    public async Task<IActionResult> UpdateParish(UpdateParish.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    
    [HttpPut("AddPriestToParish")]
    public async Task<IActionResult> AddPriestToParish(AddPriestToParish.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [HttpGet("Parish/{Id}")]
    public async Task<IActionResult> GetParish(Guid Id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetParishById.Query(Id), cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }
}