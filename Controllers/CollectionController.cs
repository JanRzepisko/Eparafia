using eparafia.Announcements.RequestModel;
using eparafia.Calendar.RequestModel;
using eparafia.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eparafia.Controllers;

[ApiController]
[Route("[controller]")]


//TODO all controller 

public class CollectionController : ControllerBase
{
    private readonly ISqlManager _sqlManager;
    private readonly IGetObject _getObject;
    private readonly ILogger<CollectionController> _logger;
    private readonly IEmailManager _emailManager;
    private readonly ITokenVerification _tokenVerification;

    private const string BaseUrl = "/collection";
    
    public CollectionController(ISqlManager sqlManager, ILogger<CollectionController> logger,
        IGetObject getObject,
        IEmailManager emailManager, ITokenVerification tokenVerification)
    {
        _sqlManager = sqlManager;
        _logger = logger;
        _getObject = getObject;
        _emailManager = emailManager;
        _tokenVerification = tokenVerification;
    }
}