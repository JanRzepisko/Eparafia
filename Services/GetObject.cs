using eparafia.Calendar.Event;
using eparafia.Calendar.EventEnums;
using eparafia.Models;
using eparafia.Priest;
using Newtonsoft.Json;

namespace eparafia.Helpers;

public class GetObject : IGetObject
{
    private readonly ISqlManager _sqlManager;
    private readonly ILogger<GetObject> _logger;
    
    public GetObject(ISqlManager sqlManager, ILogger<GetObject> logger)
    {
        _sqlManager = sqlManager;
        _logger = logger;
    }
    
    public async Task<User> GetUser(int id)
    {
        var data = await _sqlManager.Reader($"SELECT * FROM users.users WHERE id = {id};");
        if (data.Count == 0) throw new UserIsNotExist();
        
        User user = new User(data[0]["name"],data[0]["surname"],data[0]["email"],data[0]["phonenumber"],data[0]["parafia"],data[0]["id"],data[0]["adress"], data[0]["isactive"]);

        return user;
    }
    
    public Task<User> GetUser(List<Dictionary<string, dynamic>> data)
    {
        return Task.FromResult(new User(data[0]["name"],data[0]["surname"],data[0]["email"],data[0]["phonenumber"],data[0]["parafia"],data[0]["id"],data[0]["adress"], data[0]["isactive"]));
    }

    public async Task<Priest.Priest> GetPriest(int id)
    {
        var data = await _sqlManager.Reader($"SELECT * FROM users.priest WHERE id = {id};");
        if (data.Count == 0) throw new UserIsNotExist();
        
        Priest.Priest user = new Priest.Priest((PriestRole)data[0]["role"],data[0]["phonenumber"],data[0]["yearofordination"],data[0]["surname"],data[0]["email"],data[0]["name"],data[0]["id"], data[0]["parafia"]);

        return user;    
    }

    public async Task<Parafia.Parafia> GetParafia(int id)
    {
        var data = await _sqlManager.Reader($"SELECT * FROM parafia.parafia WHERE id = {id};");
        if (data.Count == 0) throw new UserIsNotExist();

        List<Priest.Priest> priests = new List<Priest.Priest>();
        var dataPriests = await _sqlManager.Reader($"SELECT id FROM users.priest WHERE parafia = {id};");

        foreach (var item in dataPriests)
        {
            priests.Add(await GetPriest(item["id"]));
        }
        
        List<User> users = new List<User>();
        var dataUsers = await _sqlManager.Reader($"SELECT id FROM users.users WHERE parafia = {id};");

        foreach (var item in dataUsers)
        {
            users.Add(await GetUser(item["id"]));
        }


        return new Parafia.Parafia(data[0]["id"], data[0]["name"], data[0]["city"], data[0]["address"], priests, data[0]["createddate"].ToString(),
            data[0]["subscriptionexpiration"], (decimal)data[0]["subscriptionprice"], users, JsonConvert.DeserializeObject(data[0]["weekcalendar"]));
    }

    public async Task<Announcements.Announcements> GetAnnouncements(int id)
    {
        var data = await _sqlManager.Reader($"SELECT * FROM parafia.announcements WHERE id = {id};");
        
        return new Announcements.Announcements(data[0]["id"], data[0]["tittle"], data[0]["content"], data[0]["date"].ToString(), data[0]["parafia"]);
    }


    public async Task<List<Event>> GetCalendar(int parafiaId)
    {
        string? jsonCalendar = (await _sqlManager.Reader($"SELECT weekcalendar FROM parafia.parafia WHERE id = {parafiaId};"))[0].ToString();

        var data = await _sqlManager.Reader($"SELECT * FROM parafia.events WHERE parafiaid = {parafiaId};");

        List<Event> calendar = new List<Event>();

        List<Event> defaultCalendar = JsonConvert.DeserializeObject(jsonCalendar) as List<Event>;

        foreach (var item in data)
        {
            calendar.Add(new SpecialEvent(item["type"], item["duration"], item["description"], DateTime.Parse(item["date"])));
        }
        foreach (var item in defaultCalendar)
        {
            calendar.Add(item);
        }
        return calendar;
    }
}