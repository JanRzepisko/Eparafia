using Eparafia.Identity.Application.Actions.Priest;
using Eparafia.Identity.Application.Actions.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;

namespace Eparafia.Identity.API.Controllers;

[ApiController]
[Route("User")]
public class UserController : BaseApiController
{
    public UserController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [Route("Login")]
    public Task<IActionResult> Login(LoginUser.Query request) => base.Endpoint(request);
    
    [HttpPost]
    [Route("RefreshToken")]
    public Task<IActionResult> RefreshToken(RefreshToken.Query request) => base.Endpoint(request);
    
    [HttpPost]
    public Task<IActionResult> RegisterUser(RegisterUser.Command request) => base.Endpoint(request);    
    
    [HttpPut]
    public Task<IActionResult> UpdateUser(UpdateUser.Command request) => base.Endpoint(request);
    
    [HttpDelete]
    public Task<IActionResult> DeleteUser(RemoveUser.Command request) => base.Endpoint(request);
}