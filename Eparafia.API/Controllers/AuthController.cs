using Eparafia.API.Models;
using Eparafia.Application.Actions.PriestAuth.Command;
using Eparafia.Application.Actions.PriestAuth.Query;
using Eparafia.Application.Actions.UserAuth.Command;
using Eparafia.Application.Actions.UserAuth.Query;
using Eparafia.Application.Services.UserProvider;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eparafia.API.Controllers;

[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("User/LoginUser")]
    public async Task<IActionResult> LoginUser(LoginUser.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }
    [HttpPost("User/RegisterUser")]
    public async Task<IActionResult> RegisterUser(RegisterUser.Command command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("User/Update")]
    public async Task<IActionResult> UpdateUser(UpdateUser.Command command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("User/ChangePhoto")]
    public async Task<IActionResult> ChangePhotoUser(ChangeUserAvatar.Command command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("User/JoinToParish")]
    public async Task<IActionResult> JoinToParishUser(JoinToParishUser.Command command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    
    [Authorize]
    [HttpDelete("User/Remove")]
    public async Task<IActionResult> RemoveUser(RemoveUser.Command command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [HttpGet("Priest/LoginUser")]
    public async Task<IActionResult> LoginPriest(LoginPriest.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }
    [HttpPost("Priest/Register")]
    public async Task<IActionResult> RegisterPriest(RegisterPriest.Command command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("Priest/Update")]
    public async Task<IActionResult> UpdatePriest(UpdatePriest.Command command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpPut("Priest/ChangePhoto")]
    public async Task<IActionResult> ChangePhotoPriest(ChangePriestAvatar.Command command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    [Authorize]
    [HttpDelete("Priest/Remove")]
    public async Task<IActionResult> RemovePriest(RemovePriest.Command command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
}