using eparafia.Announcements.RequestModel;
using eparafia.Calendar.RequestModel;
using eparafia.Carol.RequestModel;
using eparafia.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eparafia.Controllers;

[ApiController]
[Route("[controller]")]


//TODO all controller 

public class CarolController : ControllerBase
{
    private readonly ISqlManager _sqlManager;
    private readonly IGetObject _getObject;
    private readonly ILogger<CarolController> _logger;
    private readonly IEmailManager _emailManager;
    private readonly ITokenVerification _tokenVerification;

    private const string BaseUrl = "/carol";
    
    public CarolController(ISqlManager sqlManager, ILogger<CarolController> logger,
        IGetObject getObject,
        IEmailManager emailManager, ITokenVerification tokenVerification)
    {
        _sqlManager = sqlManager;
        _logger = logger;
        _getObject = getObject;
        _emailManager = emailManager;
        _tokenVerification = tokenVerification;
    }
    
    [HttpPost($"{BaseUrl}/sendInvite")]
    public async Task<IActionResult> SendInvite(SendInviteRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.User))
        {
            return StatusCode(409, "BadAccessToken");
        }
        
        
        
        return Ok();
    }
}