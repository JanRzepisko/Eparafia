using Eparafia.Application.Actions.Priest;
using Eparafia.Application.Actions.Priest.Query;
using Eparafia.Application.EventConsumerActions.Priest.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;
using Shared.BaseModels.Jwt;

namespace Eparafia.API.Controllers;

[Route("Priest")]
[ApiController]
[Authorize(JwtPolicies.Priest)]
public class PriestController : BaseApiController
{
    public PriestController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetById()
    {
        return await Endpoint(new GetPriestById.Query());
    }

    [HttpGet("FreePriests")]
    public async Task<IActionResult> GetFreePriests(string query, int page)
    {
        return await Endpoint(new GetFreePriests.Query(query, page));
    }

    [HttpDelete("RemovePriestFromParish")]
    public async Task<IActionResult> RemovePriestFromParish(LeaveParish.Command command)
    {
        return await Endpoint(command);
    }

    [HttpPut("AddPriestToParish")]
    public async Task<IActionResult> AddPriestToParish(AddPriestToParish.Command command)
    {
        return await Endpoint(command);
    }
}