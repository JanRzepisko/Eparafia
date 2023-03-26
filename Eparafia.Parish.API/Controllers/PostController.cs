using Eparafia.Application.Actions.Parish;
using Eparafia.Application.Actions.Posts.Command;
using Eparafia.Application.Actions.Posts.Query;
using Eparafia.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;
using Shared.BaseModels.Jwt;

namespace Eparafia.API.Controllers;

[ApiController]
[Authorize(JwtPolicies.Priest)]
[Produces("application/json")]
[Route("Post")]
public class PostController : Controller
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePost.Command command,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePost(UpdatePost.Command command,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }

    [HttpDelete]
    public async Task<IActionResult> UpdatePost(RemovePost.Command command,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetPost(Guid parishId, int page, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetLatestPosts.Query(parishId, page), cancellationToken);
        return Ok(ApiResponse.Success(200, result.Select(PostDTO.FromEntity)));
    }

    [AllowAnonymous]
    [HttpGet("GetPostById")]
    public async Task<IActionResult> GetPost(Guid postId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetPostById.Query(postId), cancellationToken);
        return Ok(ApiResponse.Success(200, PostDTO.FromEntity(result)));
    }
}