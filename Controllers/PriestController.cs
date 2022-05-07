using System.ComponentModel;
using BCrypt.Net;
using eparafia.Helpers;
using eparafia.Models;
using eparafia.Models.ResponsModel;
using eparafia.Priest;
using Microsoft.AspNetCore.Mvc;


namespace eparafia.Controllers;

[ApiController]
[Route("[controller]")]
public class PriestController : ControllerBase
{
    private static Random random = new Random();

    private readonly ISqlManager _sqlManager;
    private readonly IGetObject _getObject;
    private readonly ILogger<PriestController> _logger;
    private readonly IEmailManager _emailManager;
    private readonly ITokenVerification _tokenVerification;

    private const string BaseUrl = "/priest";

    public PriestController(ISqlManager sqlManager, ILogger<PriestController> logger, IGetObject getObject, IEmailManager emailManager, ITokenVerification tokenVerification)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        _sqlManager = sqlManager;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _getObject = getObject;
        _emailManager = emailManager;
        _tokenVerification = tokenVerification;
    }

    [HttpPost($"{BaseUrl}/login")]
    public async Task<IActionResult> Login(LoginUserRequestModel request)
    {
        Priest.Priest priest;

        if (await _sqlManager.IsValueExist($"SELECT id FROM users.priest WHERE email = '{request.Email}';"))
        {
            var data = (await _sqlManager.Reader($"SELECT id, password, isactive, firstlogintoken FROM users.priest WHERE email = '{request.Email}';"))[0];

            if (BCrypt.Net.BCrypt.Verify(request.Password, data["password"]))
            {
                priest = await _getObject.GetPriest(data["id"]);
            }
            else if(!data["isactive"] && request.Password == data["firstlogintoken"])
            {
                return StatusCode(409, "AccountIsNotConfigured");
            }
            else
            {
                return StatusCode(409, "BadPassword");
            }
        }
        else
        {
            return StatusCode(409, "UserIsNotExist");
        }
        
        
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string newToken = new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        newToken += "_" + priest.Id;

        newToken += "_" + DateTime.Now.ToString("dd-MM-yyyy HH':'mm':'ss");

        var tokens = (await _sqlManager.Reader($"SELECT token1, token2 FROM users.priest WHERE id = {priest.Id};"))[0];

        
        if (tokens["token1"] == "")
        {
            await _sqlManager.Execute($"UPDATE users.priest SET token1 = '{newToken}';");
        }
        else if (tokens["token2"] == "")
        {
            await _sqlManager.Execute($"UPDATE users.priest SET token2 = '{newToken}';");
        }
        else if (DateTime.Parse(tokens["token1"].ToString().Split('_')[2]) > DateTime.Parse(tokens["token2"].ToString().Split('_')[2]))
        {
            await _sqlManager.Execute($"UPDATE users.priest SET token2 = '{newToken}';");
        }
        else
        {
            await _sqlManager.Execute($"UPDATE users.priest SET token1 = '{newToken}';");
        }
        LoginPriestResponseModel response = new LoginPriestResponseModel(priest, newToken);

        return new ObjectResult(response);
        
    }
    [HttpPost($"{BaseUrl}/isUserExist")]
    public async Task<IActionResult> IsUserExist(IsUserExistRequestModel request)
    {
        if (!await _tokenVerification.UserVerification(request.Token, UserType.Priest))
        {
            return StatusCode(409, "BadAccessToken");
        }
        Priest.Priest priest;
        try
        {
            priest = await _getObject.GetPriest(request.Id);
        }
        catch (UserIsNotExist)
        {
            return StatusCode(409, "UserIsNotExist");
        }

        return new ObjectResult(priest);
    }
    [HttpPost($"{BaseUrl}/firstLogin")]
    public async Task<IActionResult> FirstLogin(FirstLoginRequestModel request)
    {
        Priest.Priest priest;

        if (await _sqlManager.IsValueExist($"SELECT id FROM users.priest WHERE email = '{request.Email}';"))
        {
            var data = (await _sqlManager.Reader($"SELECT id, password, isactive, firstlogintoken FROM users.priest WHERE email = '{request.Email}';"))[0];

            if (BCrypt.Net.BCrypt.Verify(request.Password, data["password"]))
            {
                priest = await _getObject.GetPriest(data["id"]);
            }
            else if(!data["isactive"] && request.FirstLoginToken == data["firstlogintoken"])
            {
                await _sqlManager.Execute($"UPDATE users.priest SET yearofordination = {request.YearOfOrdination} WHERE id = {data["id"]};");
                await _sqlManager.Execute($"UPDATE users.priest SET password = '{BCrypt.Net.BCrypt.HashPassword(request.Password)}' WHERE id = {data["id"]};");
                await _sqlManager.Execute($"UPDATE users.priest SET isactive = {true} WHERE id = {data["id"]};");
                
                priest = await _getObject.GetPriest(data["id"]);
            }
            else
            {
                return StatusCode(409, "BadPassword");
            }
        }
        else
        {
            return StatusCode(409, "UserIsNotExist");
        }
        
        return new ObjectResult(priest);
    }
}