using Eparafia.Administration.Application.Actions.Baptism;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;

namespace Eparafia.Administration.API.Controllers;

[ApiController]
[Route("Baptism")]
public class BaptismController : BaseApiController
{
    public BaptismController(IMediator mediator) : base(mediator) { }
    
    [HttpPost]
    public Task<IActionResult> Login(CreateBaptismRecord.Command request) => Endpoint(request);
}
