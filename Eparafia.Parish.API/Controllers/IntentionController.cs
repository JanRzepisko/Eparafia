using Eparafia.Application.Actions.Intention.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;
using Shared.BaseModels.Jwt;

namespace Eparafia.API.Controllers;

[ApiController]
[Authorize(JwtPolicies.Priest)]
[Produces("application/json")]
[Route("Intention")]
public class IntentionController : Controller
{
    private readonly IMediator _mediator;

    public IntentionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIntention(AddIntention.Command command,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }

    [Authorize(JwtPolicies.User)]
    [HttpPost("BuyIntention")]
    public async Task<IActionResult> BuyIntention(BuyIntention.Command command,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }

    [HttpPut]
    public async Task<IActionResult> EditIntention(UpdateIntention.Command command,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }

    [HttpPut("RefreshDate")]
    public async Task<IActionResult> RefreshIntentionDate(RefreshIntentionDate.Command command,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveIntention(RemoveIntention.Command command,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
}