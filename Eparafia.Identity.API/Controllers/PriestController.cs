using Eparafia.Identity.Application.Actions.Priest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;
using Shared.BaseModels.Jwt;

namespace Eparafia.Identity.API.Controllers;

[Authorize(JwtPolicies.Priest)]
[ApiController]
[Route("Priest")]
public class PriestController : BaseApiController
{
    public PriestController(IMediator mediator) : base(mediator) { }

    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public Task<IActionResult> Login(LoginPriest.Query request) => Endpoint(request);

    [AllowAnonymous]
    [HttpPost]
    public Task<IActionResult> RegisterUser(RegisterPriest.Command request) => Endpoint(request);    
    
    [HttpPost]
    [Route("RefreshToken")]
    public Task<IActionResult> RefreshToken(RefreshToken.Query request) => Endpoint(request);
    
    [HttpPut]
    public Task<IActionResult> UpdateUser(UpdatePriest.Command request) => Endpoint(request);
    
    [HttpDelete]
    public Task<IActionResult> DeleteUser(RemovePriest.Command request) => Endpoint(request);
}