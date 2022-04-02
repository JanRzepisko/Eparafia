using eparafia.Models;

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
    
    public async Task<User> GetUser(List<Dictionary<string, dynamic>> data)
    {
        return new User(data[0]["name"],data[0]["surname"],data[0]["email"],data[0]["phonenumber"],data[0]["parafia"],data[0]["id"],data[0]["adress"], data[0]["isactive"]);
    }
}