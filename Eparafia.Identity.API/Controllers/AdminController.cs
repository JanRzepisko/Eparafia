using Eparafia.Identity.Application.Actions.Priest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;

namespace Eparafia.Identity.API.Controllers;

[ApiController]
[Route("User")]
public class AdminController : BaseApiController
{
    public AdminController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [Route("Login")]
    public Task<IActionResult> Login(LoginPriest.Query request) => base.Endpoint(request);
    
    [HttpPost]
    [Route("RefreshToken")]
    public Task<IActionResult> RefreshToken(RefreshToken.Query request) => base.Endpoint(request);
    
    [HttpPost]
    public Task<IActionResult> RegisterUser(RegisterPriest.Command request) => base.Endpoint(request);    
    
    [HttpPut]
    public Task<IActionResult> UpdateUser(UpdatePriest.Command request) => base.Endpoint(request);
    
    [HttpDelete]
    public Task<IActionResult> DeleteUser(RemovePriest.Command request) => base.Endpoint(request);
}