using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Shared.BaseModels.ApiControllerModels;

public class BaseApiController : Controller
{
    private readonly IMediator _mediator;

    public BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<IActionResult> Endpoint(IRequest<Unit> request, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }

    protected async Task<IActionResult> Endpoint<T>(IRequest<T> request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(ApiResponse.Success(200, result));
    }
}