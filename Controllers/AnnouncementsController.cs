using System.ComponentModel;
using eparafia.Helpers;
using eparafia.Models;
using Microsoft.AspNetCore.Mvc;

namespace eparafia.Controllers;

[ApiController]
[Route("[controller]")]
public class AnnouncementsController : ControllerBase
{
    private readonly ISqlManager _sqlManager;
    private readonly IGetObject _getObject;
    private readonly ILogger<AnnouncementsController> _logger;
    private readonly IEmailManager _emailManager;

    private const string BaseUrl = "/user";

    public AnnouncementsController(ISqlManager sqlManager, ILogger<AnnouncementsController> logger, IGetObject getObject,
        IEmailManager emailManager)
    {
        _sqlManager = sqlManager;
        _logger = logger;
        _getObject = getObject;
        _emailManager = emailManager;
    }

    [HttpPost($"{BaseUrl}/add")]
    public async Task<IActionResult> Add(RegisterUserRequestModel request)
    {
        return Ok();
    }
    [HttpPost($"{BaseUrl}/edit")]
    public async Task<IActionResult> Edit(RegisterUserRequestModel request)
    {
        return Ok();
    }
    [HttpPost($"{BaseUrl}/get")]
    public async Task<IActionResult> Get(RegisterUserRequestModel request)
    {
        return Ok();
    }
}
