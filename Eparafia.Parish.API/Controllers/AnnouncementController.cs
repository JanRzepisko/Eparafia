using Eparafia.Application.Actions.Announcements.Command;
using Eparafia.Application.Actions.Announcements.Query;
using Eparafia.Application.Actions.Parish;
using Eparafia.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseModels.ApiControllerModels;
using Shared.BaseModels.Jwt;

namespace Eparafia.API.Controllers;

[ApiController]
[Authorize(JwtPolicies.Priest)]
[Route("Announcement")]
public class AnnouncementController : Controller
{
    private readonly IMediator _mediator;

    public AnnouncementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnnouncement(AnnouncementsCreate.Command command,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAnnouncement([FromBody] AnnouncementsUpdate.Command command)
    {
        await _mediator.Send(command);
        return Ok(ApiResponse.Success(200));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAnnouncement([FromBody] AnnouncementsRemove.Command command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse.Success(200));
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAnnouncement(Guid parishId, int page,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new AnnouncementsGet.Query(parishId, page), cancellationToken);
        return Ok(ApiResponse.Success(200, AnnouncementsDTO.FromEntity(result)));
    }
    [AllowAnonymous]
    [HttpGet("ById")]
    public async Task<IActionResult> GetAnnouncementById(Guid announcementId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAnnouncementsById.Query(announcementId), cancellationToken);
        return Ok(ApiResponse.Success(200, AnnouncementsDTO.FromEntity(result)));
    }

    [AllowAnonymous]
    [HttpGet("Search")]
    public async Task<IActionResult> SearchInAnnouncement(Guid parishId, string? query, int page,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new SearchInAnnouncements.Query(parishId, query, page), cancellationToken);
        return Ok(ApiResponse.Success(200, AnnouncementRecordDTO.FromEntity(result)));
    }
    
    [HttpGet("BeforePublish")]
    public async Task<IActionResult> GetAnnouncementBeforePublish(int page, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAnnouncementsBeforePublish.Query(page), cancellationToken);
        return Ok(ApiResponse.Success(200, AnnouncementsDTO.FromEntity(result)));
    }
}