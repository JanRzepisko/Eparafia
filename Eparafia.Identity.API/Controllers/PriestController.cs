using Eparafia.Identity.Application.Actions.Priest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;

namespace Eparafia.Identity.API.Controllers;

[ApiController]
[Route("Priest")]
public class PriestController : BaseApiController
{
    public PriestController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [Route("Login")]
    public Task<IActionResult> Login(LoginPriest.Query request) => Endpoint(request);
    [HttpPost]
    [Route("RefreshToken")]
    public Task<IActionResult> RefreshToken(RefreshToken.Query request) => Endpoint(request);
    
    [HttpPost]
    public Task<IActionResult> RegisterUser(RegisterPriest.Command request) => Endpoint(request);    
    
    [HttpPut]
    public Task<IActionResult> UpdateUser(UpdatePriest.Command request) => Endpoint(request);
    
    [HttpDelete]
    public Task<IActionResult> DeleteUser(RemovePriest.Command request) => Endpoint(request);
}