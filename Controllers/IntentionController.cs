using eparafia.Announcements.RequestModel;
using eparafia.Calendar.RequestModel;
using eparafia.Helpers;
using eparafia.Intention.RequestModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eparafia.Controllers;

[ApiController]
[Route("[controller]")]
public class IntentionController : ControllerBase
{
    private readonly ISqlManager _sqlManager;
    private readonly IGetObject _getObject;
    private readonly ILogger<IntentionController> _logger;
    private readonly IEmailManager _emailManager;
    private readonly ITokenVerification _tokenVerification;

    private const string BaseUrl = "/intention";
    
    public IntentionController(ISqlManager sqlManager, ILogger<IntentionController> logger, IGetObject getObject, IEmailManager emailManager, ITokenVerification tokenVerification)
    {
        _sqlManager = sqlManager;
        _logger = logger;
        _getObject = getObject;
        _emailManager = emailManager;
        _tokenVerification = tokenVerification;
    }

    [HttpPost($"{BaseUrl}/add")]
    public async Task<IActionResult> AddIntention(AddIntentionRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }

        await _sqlManager.Execute($"INSERT INTO parafia.intention VALUES({request.ParafiaId}, '{DateTime.Parse(request.Date).ToFileTimeUtc().ToString()}', {(int) request.Type}, '{request.Value}');");
        
        return Ok();
    }    
    
    [HttpPost($"{BaseUrl}/addNovena")]
    public async Task<IActionResult> AddNovena(AddNovenaRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }

        //TODO: new novena system + from who??
        
        await _sqlManager.Execute($"INSERT INTO parafia.novena VALUES({request.ParafiaId}, '{DateTime.Parse(request.StartDate).ToString("G")}', {request.Count}, '{request.Value}');");
        
        return Ok();
    }
    
    [HttpPost($"{BaseUrl}/generateNovena")]
    public async Task<IActionResult> GenerateNovena(GenerateNovenaRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }

        var date = await _sqlManager.Reader($"SELECT * FROM parafia.novena WHERE parafiaid = {request.ParafiaId};");

        List<string> intentions = new List<string>();
        int dayOfWeek = DayOfWeek.Monday - DateTime.Today.DayOfWeek;
        DateTime monday = DateTime.Today.AddDays(dayOfWeek).AddDays(request.Week * 7);
        
        foreach (var item in date)
        {
            DateTime startDate = DateTime.Parse(item["startdate"]);

            if (startDate.AddDays(7 * item["count"]) > monday && startDate <= monday)
            {
                intentions.Add(item["value"]);
            }
        }

        return new ObjectResult(intentions);
    }
}