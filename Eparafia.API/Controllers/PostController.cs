using Eparafia.API.Models;
using Eparafia.Application.Actions.Parish;
using Eparafia.Application.DTOs;
using Eparafia.Application.Entities;
using Eparafia.Application.Services.Jwt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    
    [HttpPost("Post")]
    public async Task<IActionResult> CreatePost(CreatePost.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    
    [HttpPut("Post")]
    public async Task<IActionResult> UpdatePost(UpdatePost.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    
    [HttpDelete("Post")]
    public async Task<IActionResult> UpdatePost(RemovePost.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    
    [AllowAnonymous]
    [HttpGet("Post")]
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