using eparafia.Calendar.Event;
using eparafia.Models;
using eparafia.Priest;

namespace eparafia.Parafia;

public class Parafia
{
    public Parafia(int id, string name, string city, string address, List<Priest.Priest> priests, string createdDate, string subscriptionExpiration, decimal subscriptionPrice, List<User> users, List<DefaultEvent> defaultWeek)
    {
        Id = id;
        Name = name;
        City = city;
        Address = address;
        Priests = priests;
        CreatedDate = createdDate;
        SubscriptionExpiration = subscriptionExpiration;
        SubscriptionPrice = subscriptionPrice;
        Users = users;
        DefaultWeek = defaultWeek;
    }

    public int Id { get; }
    public string Name { get; }
    public string City { get; }
    public string Address { get; }
    public List<Priest.Priest> Priests { get; }
    public List<User> Users { get; }
    public string CreatedDate { get; }
    public string SubscriptionExpiration { get; }
    public decimal SubscriptionPrice { get; }
    public List<DefaultEvent> DefaultWeek { get; }
}