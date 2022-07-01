using eparafia.Announcements.RequestModel;
using eparafia.Calendar.RequestModel;
using eparafia.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eparafia.Controllers;

[ApiController]
[Route("[controller]")]
public class CalendarController : ControllerBase
{
    private readonly ISqlManager _sqlManager;
    private readonly IGetObject _getObject;
    private readonly ILogger<CalendarController> _logger;
    private readonly IEmailManager _emailManager;
    private readonly ITokenVerification _tokenVerification;

    private const string BaseUrl = "/calendar";
    
    public CalendarController(ISqlManager sqlManager, ILogger<CalendarController> logger,
        IGetObject getObject,
        IEmailManager emailManager, ITokenVerification tokenVerification)
    {
        _sqlManager = sqlManager;
        _logger = logger;
        _getObject = getObject;
        _emailManager = emailManager;
        _tokenVerification = tokenVerification;
    }
    
    [HttpPost($"{BaseUrl}/editDefault")]
    public async Task<IActionResult> EditDefault(EditDefaultEventRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }

        string weekJson = JsonConvert.SerializeObject(request.DefaultWeek);

        await _sqlManager.Execute($"UPDATE parafia.parafia SET weekcalendar = '{weekJson}' WHERE id = {request.ParafiaId}");
        
        return Ok();
    }

    [HttpPost($"{BaseUrl}/addSpecial")]
    public async Task<IActionResult> AddSpecial(AddSpecialEventRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }

        int id = 0;
        while (true)
        {
            Random rand = new Random();
            id = rand.Next(10000000, 99999999);          
            if (!await _sqlManager.IsValueExist($"SELECT * FROM parafia.events WHERE id = {id};"))
                break;
        }
        
        
        await _sqlManager.Execute($"INSERT INTO parafia.events VALUES({id}, {request.ParafiaId}, '{request.SpecialEvent.Date}', {(int)request.SpecialEvent.Type}, {request.SpecialEvent.Duration}, '{request.SpecialEvent.Description}');");
        return Ok();
    }
    
    [HttpPost($"{BaseUrl}/editSpecial")]
    public async Task<IActionResult> EditSpecial(EditSpecialEventRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }

        var data = await _sqlManager.Reader($"SELECT * FROM parafia.events WHERE id = {request.EventId}");
        if (data.Count == 0) return StatusCode(409, "EventNotFound");
        string key = data[0].Keys.ElementAt((int) request.EditType + 2).ToString();

        string value = "";

        if (!int.TryParse(request.Value as string, out int a)) value = "'" + request.Value + "'";

        await _sqlManager.Execute($"UPDATE parafia.events SET {key} = {value};");
        
        return Ok();
    }

    [HttpPost($"{BaseUrl}/deleteSpecial")]
    public async Task<IActionResult> DeleteSpecial(DeleteSpecialEventRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }

        await _sqlManager.Execute($"DELETE FROM parafia.events WHERE id = {request.Id};");

        return Ok();
    }


    //User Endpoint
    [HttpPost($"{BaseUrl}/getCalendar")]
    public async Task<IActionResult> GetCalendar(GetCalendarRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.User)  && !await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }

        return new ObjectResult(await _getObject.GetCalendar(request.ParafiaId, request.Week));
    }
}