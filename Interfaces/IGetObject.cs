using eparafia.Calendar.Event;
using eparafia.Models;

namespace eparafia;

public interface IGetObject
{
    public Task<User> GetUser(int id);
    public Task<User> GetUser(List<Dictionary<string, dynamic>> data);
    public Task<Priest.Priest> GetPriest(int id);
    public Task<Parafia.Parafia> GetParafia(int id);
    public Task<Announcements.Announcements> GetAnnouncements(int id);
    public Task<List<SpecialEvent>> GetCalendar(int parafiaId, int week);
}