using eparafia.Calendar.Event;
using eparafia.Carol;
using eparafia.Models;
using eparafia.Intention;

namespace eparafia;

public interface IGetObject
{
    public Task<User.User> GetUser(int id);
    public Task<User.User> GetUser(List<Dictionary<string, dynamic>> data);
    public Task<Priest.Priest> GetPriest(int id);
    public Task<Parafia.Parafia> GetParafia(int id);
    public Task<Announcements.Announcements> GetAnnouncements(int id);
    public Task<List<SpecialEvent>> GetCalendar(int parafiaId, int week);
    public Task<UserCarol> GetCarolUser(int userId);
    public Task<Intention.Intention> GetIntention(DateTime date, int parafiaid);
}