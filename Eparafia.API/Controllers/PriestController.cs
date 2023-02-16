using Eparafia.Application.Actions.Priest;
using Eparafia.Application.Actions.Priest.Query;
using Eparafia.Application.EventConsumerActions.Priest.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;

namespace Eparafia.API.Controllers;

[Route("Priest")]
[ApiController]
public class PriestController : BaseApiController
{
    public PriestController(IMediator mediator) : base(mediator){}
        
    [HttpGet]
    public async Task<IActionResult> GetById() => await base.Endpoint(new GetPriestById.Query());
    
    [HttpGet("FreePriests")]
    public async Task<IActionResult> GetFreePriests(string query, int page) => await base.Endpoint(new GetFreePriests.Query(query, page));    

    [HttpDelete("RemovePriestFromParish")]
    public async Task<IActionResult> RemovePriestFromParish(LeaveParish.Command command) => await base.Endpoint(command);
    [HttpPut("AddPriestToParish")]
    public async Task<IActionResult> AddPriestToParish(AddPriestToParish.Command command) => await base.Endpoint(command);
}