using Eparafia.API.Models;
using Eparafia.Application.Actions.PriestAuth.Command;
using Eparafia.Application.Actions.PriestAuth.Query;
using Eparafia.Application.Actions.UserAuth.Command;
using Eparafia.Application.Actions.UserAuth.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eparafia.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("Auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("User/Login")]
    public async Task<IActionResult> LoginUser(LoginUser.Query query, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }
    
    [HttpPost("User/Register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUser.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("User/Update")]
    public async Task<IActionResult> UpdateUser([FromBody]UpdateUser.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("User/JoinToParish")]
    public async Task<IActionResult> JoinToParishUser(JoinToParishUser.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    
    [Authorize]
    [HttpDelete("User/Remove")]
    public async Task<IActionResult> RemoveUser(RemoveUser.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [HttpPost("Priest/Login")]
    public async Task<IActionResult> LoginPriest(LoginPriest.Query query, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }
    [HttpPost("Priest/Register")]
    public async Task<IActionResult> RegisterPriest(RegisterPriest.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("Priest/Update")]
    public async Task<IActionResult> UpdatePriest(UpdatePriest.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpDelete("Priest/Remove")]
    public async Task<IActionResult> RemovePriest(RemovePriest.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
}