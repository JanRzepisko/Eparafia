using eparafia.Helpers;
using eparafia.Models;
using eparafia.Parafia.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace eparafia.Controllers;

[ApiController]
[Route("[controller]")]
public class ParafiaController
{
    private readonly ISqlManager _sqlManager;
    private readonly IGetObject _getObject;
    private readonly ILogger<UserController> _logger;
    private readonly IEmailManager _emailManager;

    private const string BaseUrl = "/parafia";

    public ParafiaController(ISqlManager sqlManager, ILogger<UserController> logger, IGetObject getObject, IEmailManager emailManager)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        _sqlManager = sqlManager;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _getObject = getObject;
        _emailManager = emailManager;
    }

    [HttpPost($"{BaseUrl}/register")]
    public async Task<IActionResult> Register(RegisterParafiaRequestModel request)
    {
        int id;         
        int parafiaId;         
        while (true)
        {
            Random rand = new Random();
            parafiaId = rand.Next(10000000, 99999999);          
            if (!await _sqlManager.IsValueExist($"SELECT * FROM parafia.parafia WHERE id = {parafiaId};"))
                break;
        }
        
        await _sqlManager.Execute($"INSERT INTO parafia.parafia VALUES({parafiaId}, '{request.Name}', '{request.City}', NOW(), '{request.SubscriptionExpiration}', {request.SubscriptionPrice}, '{request.Address}');");
        
        foreach (var item in request.Priests)
        {
            while (true)
            {
                Random rand = new Random();
                id = rand.Next(10000000, 99999999);          
                if (!await _sqlManager.IsValueExist($"SELECT * FROM users.priest WHERE id = {id};"))
                    break;
            }

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string firstLoginToken = new String(stringChars);
            
            await _sqlManager.Execute($"INSERT INTO users.priest VALUES({id}, '{item.Name}', '{item.SurName}', '{item.Email}', '{item.PhoneNumber}', 0,0,'{BCrypt.Net.BCrypt.HashPassword("Password")}',{parafiaId},false, '{firstLoginToken}');");
        }

        await _sqlManager.Execute($"create table announcements.{parafiaId} (id int, content varchar, date date);");
        
        return new OkResult();
    }

    [HttpPost($"{BaseUrl}/get")]
    public async Task<IActionResult> Get(GetParafiaRequestModel request)
    {
        return new ObjectResult(await _getObject.GetParafia(request.Id));
    }
    
}