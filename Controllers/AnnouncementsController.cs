using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using eparafia.Announcements.RequestModel;
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

    private const string BaseUrl = "/announcements";

    public AnnouncementsController(ISqlManager sqlManager, ILogger<AnnouncementsController> logger,
        IGetObject getObject,
        IEmailManager emailManager)
    {
        _sqlManager = sqlManager;
        _logger = logger;
        _getObject = getObject;
        _emailManager = emailManager;
    }

    [HttpPost($"{BaseUrl}/add")]
    public async Task<IActionResult> Add(AddAnnouncementRequestModel request)
    {
        Random rand = new Random();
        int id = rand.Next(1000000, 9999999);
        while (true)
        {
            id = rand.Next(1000000, 9999999);
            if (!await _sqlManager.IsValueExist($"SELECT id FROM parafia.announcements WHERE id = {id}"))
            {
                break;
            }
        }

        await _sqlManager.Execute(
            $"INSERT INTO parafia.announcements VALUES({id}, '{request.Content}', '{request.Title}', NOW(), {request.ParafiaId});");

        return Ok();
    }

    [HttpPost($"{BaseUrl}/edit")]
    public async Task<IActionResult> Edit(EditAnnouncementRequestModel request)
    {
        switch (request.Mode)
        {
            case SettingsMode.Content:
            {
                await _sqlManager.Execute(
                    $"UPDATE parafia.announcements SET content = '{request.Content}' WHERE id = {request.Id}");
                break;
            }
            case SettingsMode.Tittle:
            {
                await _sqlManager.Execute(
                    $"UPDATE parafia.announcements SET tittle = '{request.Content}' WHERE id = {request.Id}");
                break;
            }
        }

        return Ok();
    }

    [HttpPost($"{BaseUrl}/get")]
    public async Task<IActionResult> Get(GetAnnouncementRequestModel request)
    {
        int objectsPerPage = 2;

        var data = await _sqlManager.Reader(
            $"SELECT * FROM parafia.announcements WHERE parafia = {request.ParafiaId} ORDER BY date;");

        List<Announcements.Announcements> announcementsList = new List<Announcements.Announcements>();

        for (int i = objectsPerPage * request.Page; i < (objectsPerPage * request.Page) + objectsPerPage; i++)
        {
            try
            {
                announcementsList.Add(await _getObject.GetAnnouncements(data[i]["id"]));
            }
            catch
                (ArgumentOutOfRangeException)
            {
                break;
            }
        }
        return new ObjectResult(announcementsList);
    }
}
