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
[Route("User")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser(LoginUser.Query query, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUser.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("Update")]
    public async Task<IActionResult> UpdateUser([FromBody]UpdateUser.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("JoinToParish")]
    public async Task<IActionResult> JoinToParishUser(JoinToParishUser.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    
    [Authorize]
    [HttpDelete("Remove")]
    public async Task<IActionResult> RemoveUser(RemoveUser.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
}