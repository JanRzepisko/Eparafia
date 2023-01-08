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
[Route("Parish")]
public class AnnouncementController : Controller
{
    private readonly IMediator _mediator;

    public AnnouncementController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("Announcement")]
    public async Task<IActionResult> CreateAnnouncement(AnnouncementsCreate.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    
    [HttpPut("Announcement")]
    public async Task<IActionResult> UpdateAnnouncement(AnnouncementsUpdate.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    
    [HttpDelete("Announcement")]
    public async Task<IActionResult> UpdateAnnouncement(AnnouncementsRemove.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }    
    [AllowAnonymous]
    [HttpGet("Announcement")]
    public async Task<IActionResult> GetAnnouncement(Guid parishId, int page, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new AnnouncementsGet.Query(parishId, page), cancellationToken);
        return Ok(ApiResponse.Success(200, AnnouncementsDTO.FromEntity(result)));
    }    
    [AllowAnonymous]
    [HttpGet("Search")]
    public async Task<IActionResult> SearchInAnnouncement(Guid parishId, string? query, int page, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new SearchInAnnouncements.Query(parishId, query, page), cancellationToken);
        return Ok(ApiResponse.Success(200, AnnouncementRecordDTO.FromEntity(result)));
    }
}