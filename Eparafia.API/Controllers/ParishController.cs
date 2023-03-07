using Eparafia.Application.Actions.Parish;
using Eparafia.Application.Actions.Parish.Command;
using Eparafia.Application.Actions.Parish.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;
using Shared.BaseModels.Jwt;

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

    [HttpPost]
    public async Task<IActionResult> CreateParish(CreateParish.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveParish(RemoveParish.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    
    [HttpPut]
    public async Task<IActionResult> UpdateParish(UpdateParish.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    

    [HttpGet]
    public async Task<IActionResult> GetParish(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetParishById.Query(), cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }
    
    [AllowAnonymous]
    [HttpGet("GetParishByShortName")]
    public async Task<IActionResult> GetParishByShortName(string shortName, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetParishByShortName.Query(shortName), cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }    
    
    [AllowAnonymous]
    [HttpGet("GetAllParishShortNames")]
    public async Task<IActionResult> GetAllParishShortNames(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAllParishShortNames.Query(), cancellationToken);
        return Ok(ApiResponse.Success(200, result.Select(c => c.ShortName)));
    }
}