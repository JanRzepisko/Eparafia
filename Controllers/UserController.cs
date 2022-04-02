using System.ComponentModel;
using BCrypt.Net;
using eparafia.Helpers;
using eparafia.Models;
using Microsoft.AspNetCore.Mvc;


namespace eparafia.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ISqlManager _sqlManager;
    private readonly IGetObject _getObject;
    private readonly ILogger<UserController> _logger;
    private readonly IEmailManager _emailManager;
    
    private const string BaseUrl = "/user";

    public UserController(ISqlManager sqlManager, ILogger<UserController> logger, IGetObject getObject, IEmailManager emailManager)
    {
        _sqlManager = sqlManager;
        _logger = logger;
        _getObject = getObject;
        _emailManager = emailManager;
    }

    [HttpPost($"{BaseUrl}/register")]
    public async Task<IActionResult> Register(RegisterUserRequestModel request)
    {
        if (!await request.Validate())
        {
            return StatusCode(409, "EmailOrPhoneIsExist");
        }
        int id;         
        while (true)
        {
            Random rand = new Random();
            id = rand.Next(10000000, 99999999);          
            if (!await _sqlManager.IsValueExist($"SELECT * FROM users.users WHERE id = {id};"))
                break;
        }
        
        await _sqlManager.Execute($"INSERT INTO users.users VALUES({id}, '{request.Name}', '{request.SurName}', '{request.PhoneNumber}', '{request.Email}', 0, NOW(), ' ', '{BCrypt.Net.BCrypt.HashPassword(request.Password, SaltRevision.Revision2Y)}', false);");

        User user;
        
        try
        {
            user = await _getObject.GetUser(id);
        }
        catch (UserIsNotExist)
        {
            return StatusCode(409, "UnexpectedException");
        }
        await _emailManager.SendEmail(user.Email);

        return new ObjectResult(user);
    }
    
    [HttpPost($"{BaseUrl}/login")]
    public async Task<IActionResult> Login(LoginUserRequestModel request)
    {
        await request.Validate();
        User user;

        if (await _sqlManager.IsValueExist($"SELECT id FROM users.users WHERE email = '{request.Email}';"))
        {
            var data = (await _sqlManager.Reader($"SELECT id, password, isactive FROM users.users WHERE email = '{request.Email}';"))[0];

            if (BCrypt.Net.BCrypt.Verify(request.Password, data["password"]))
            {
                user = await _getObject.GetUser(data["id"]);
            }
            else
            {
                return StatusCode(409, "BadPassword");
            }
            
            if (!data["isactive"])
            {
                return StatusCode(409, "UserIsNotActive");
            }
        }
        else
        {
            return StatusCode(409, "UserIsNotExist");
        }
        
        return new ObjectResult(user);
    }

    [HttpPost($"{BaseUrl}/isUserExist")]
    public async Task<IActionResult> IsUserExist(IsUserExistRequestModel request)
    {
        User user;
        try
        {
            user = await _getObject.GetUser(request.Id);
        }
        catch (UserIsNotExist)
        {
            return StatusCode(409, "UserIsNotExist");
        }

        return new ObjectResult(user);
    }

    [HttpPost($"{BaseUrl}/settings")]
    public async Task<IActionResult> SetSettings(SettingsRequestModel request)
    {
        if (!await _sqlManager.IsValueExist($"SELECT id FROM users.users WHERE id = {request.Id}"))
            return StatusCode(409, "UserIsNotExist");
        
        switch (request.Mode)
        {
            case UserSettingsMode.Name:
            {
                await _sqlManager.Execute($"UPDATE users.users SET name = '{request.Value}' WHERE id = {request.Id};");
                break;
            }
            case UserSettingsMode.Surname:
            {
                await _sqlManager.Execute($"UPDATE users.users SET surname = '{request.Value}' WHERE id = {request.Id};");
                break;
            }
            case UserSettingsMode.Email:
            {
                await _sqlManager.Execute($"UPDATE users.users SET email = '{request.Value}' WHERE id = {request.Id};");
                break;
            }            
            case UserSettingsMode.PhoneNumber:
            {
                await _sqlManager.Execute($"UPDATE users.users SET phonenumber = '{request.Value}' WHERE id = {request.Id};");
                break;
            }
            case UserSettingsMode.Address:
            {
                await _sqlManager.Execute($"UPDATE users.users SET adress = '{request.Value}' WHERE id = {request.Id};");
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        return Ok();
    }


}