using Eparafia.Bible.Application.Actions.Command;
using Eparafia.Bible.Application.Actions.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;

namespace Eparafia.Bible.API.Controllers;

[ApiController]
[Route("Bible")]
public class BibleController : BaseApiController
{
    public BibleController(IMediator mediator) : base(mediator) { }
    
    [HttpPost]
    public Task<IActionResult> CreateDay(CreateDay.Command request) => base.Endpoint(request);            
    [HttpGet]
    public Task<IActionResult> Get() => base.Endpoint(new Get.Command());        
 
}