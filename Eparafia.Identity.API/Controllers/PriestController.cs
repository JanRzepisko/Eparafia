using Eparafia.Identity.Application.Actions.Priest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;

namespace Eparafia.Identity.API.Controllers;

[ApiController]
[Route("Priest")]
[Authorize]
public class PriestController : BaseApiController
{
    public PriestController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Login")]
    public Task<IActionResult> Login(LoginPriest.Query request)
    {
        return Endpoint(request);
    }

    [HttpPost]
    [Route("RefreshToken")]
    public Task<IActionResult> RefreshToken(RefreshToken.Query request)
    {
        return Endpoint(request);
    }

    [HttpPost]
    [AllowAnonymous]
    public Task<IActionResult> RegisterUser(RegisterPriest.Command request)
    {
        return Endpoint(request);
    }

    [HttpPut]
    public Task<IActionResult> UpdateUser(UpdatePriest.Command request)
    {
        return Endpoint(request);
    }

    [HttpDelete]
    public Task<IActionResult> DeleteUser(RemovePriest.Command request)
    {
        return Endpoint(request);
    }
}