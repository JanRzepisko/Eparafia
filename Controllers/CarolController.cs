using System.Net.Sockets;
using eparafia.Announcements.RequestModel;
using eparafia.Calendar.RequestModel;
using eparafia.Carol;
using eparafia.Carol.RequestModel;
using eparafia.Helpers;
using eparafia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eparafia.Controllers;

[ApiController]
[Route("[controller]")]


//TODO all controller 

public class CarolController : ControllerBase
{
    private const int MinutesPerCarol = 20; //Średnia minut na kolęde
    
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
        
        User user = await _getObject.GetUser(request.Id);

        if (user.Email == "") return StatusCode(409, "EmailIsNotSet");
        if (user.Adress == "") return StatusCode(409, "AddressIsNotSet");
        if (user.PhoneNumber == "") return StatusCode(409, "PhoneNumberIsNotSet"); 
        
        if (await _sqlManager.IsValueExist($"SELECT * FROM parafia.carol WHERE id = {user.Id};"))
        {
            return StatusCode(409, "InviteIsExist");
        }
        await _sqlManager.Execute($"INSERT INTO parafia.carol VALUES({user.Id}, {user.Parafia}, NOW());");
        return Ok();
    }

    [HttpPost($"{BaseUrl}/generate")]
    public async Task<IActionResult> GenerateCarol(GenerateCarolRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }
        
        Priest.Priest priest = await _getObject.GetPriest(request.Id);

        var data = await _sqlManager.Reader($"SELECT * FROM parafia.carol WHERE parafiaid = {priest.Parafia} ORDER BY dateofinvite ;");
        
        List<InviteModel> invites = new List<InviteModel>();

        string address = "";
        
        foreach (var item in data)
        {
            User user = await _getObject.GetUser(item["id"]);
            invites.Add(new InviteModel(user.Adress, 0, user.Id));
            address += "|" + user.Adress;
        }
        
        Parafia.Parafia parafia = await _getObject.GetParafia(priest.Parafia);
        Uri apiReq = new Uri("https://maps.googleapis.com/maps/api/directions/json?destination=" + parafia.Address +
                             "&origin=" +
                             parafia.Address + "&waypoints=optimize:true" + address +
                             "&key=AIzaSyB17h7tVIEnND8kQ14kbDT9JMGgIpSpthk&mode=walking");

        //Uri testReq = new Uri("https://maps.googleapis.com/maps/api/directions/json?destination=Zielona 3, 32-050 Skawina&origin=Kościelna 3, 32-050 Skawina&waypoints=optimize:true|Kościelna 3, 32-050 SkawinaPopiełuszki 7, 32-050 Skawina|Słoneczna 10, 32-050 Skawina|Jarosława Dąbrowskiego 5, 32-050 Skawina&key=AIzaSyB17h7tVIEnND8kQ14kbDT9JMGgIpSpthk&mode=walking");
         
        HttpClient client = new HttpClient();
        HttpResponseMessage responseMsg = await client.GetAsync(apiReq);
        string stringResponse = await responseMsg.Content.ReadAsStringAsync();
        dynamic response = JsonConvert.DeserializeObject(stringResponse)!;
        int[]? waypointOrder = (response["routes"][0]["waypoint_order"] as JArray)?.ToObject<int[]>();
        for (int i = 0; i < invites.Count; i++)
        {
            invites[waypointOrder[i]].Index = i;
        }
        invites = invites.OrderBy(x => x.Index).ToList();

        int parafiaId = priest.Parafia;

        try
        {
            await _sqlManager.Execute($"CREATE TABLE carol.p{parafiaId} (userid int, address varchar, priest int, date varchar, index int, hasdone bool, carolid int);");
        }
        catch (Exception)
        {

        }

        DateTime startCarol = DateTime.Parse(request.StartCarol.Split(' ')[0] + " 00:00");
        
        
        List<InviteModel> carol = new List<InviteModel>();
        
        //Set Carol into priest and date


        for (int day = 0, carols = 0; invites.Count != carols; day++)
        {
            for (int i = 0; i < request.PriestCalendar.Count; i++)
            {
                for (int j = 1; DateTime.Parse(request.PriestCalendar[i].Calendar[day].StartHour).AddMinutes(MinutesPerCarol * j-1) < DateTime.Parse(request.PriestCalendar[i].Calendar[day].EndHour); j++)
                {
                    //Dodawanie do listy
                    
                    carol.Add(new InviteModel(invites[carols].Address, invites[carols].Index, invites[carols].Id,
                        request.PriestCalendar[i].PriestId,
                        startCarol.AddDays(day).Add(TimeSpan.Parse(request.PriestCalendar[i].Calendar[day].StartHour))
                            .AddMinutes(MinutesPerCarol * (j-1))));

                    carols += 1;
                    if(carols > invites.Count - 1) break;
                }
                if(carols > invites.Count - 1) break;
            }
        }
        
        Random rand = new Random();
        int carolId;
        
        foreach (var item in carol)
        {
            do
            {
                carolId = rand.Next(1000000, 9999999);
            } while (await _sqlManager.IsValueExist($"SELECT * FROM carol.p{parafiaId} WHERE carolid = {carolId};"));

            await _sqlManager.Execute($"INSERT INTO carol.p{parafiaId} VALUES ({item.Id}, '{item.Address}', {item.Priest}, '{item.Date}', {item.Index}, false, {carolId})");
        }

        return Ok();
    }

    [HttpPost($"{BaseUrl}/getCarolUser")]
    public async Task<IActionResult> GetCarolUser(GetCarol request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.User))
        {
            return StatusCode(409, "BadAccessToken");
        }
        return new ObjectResult(await _getObject.GetCarolUser(request.Id));
    }
    
    [HttpPost($"{BaseUrl}/getCarolPriest")]
    public async Task<IActionResult> GetCarolPriest(GetCarol request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }
        
        await _getObject.GetPriest(request.Id);
        return Ok();
    }
}
