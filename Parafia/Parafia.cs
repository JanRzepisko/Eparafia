using eparafia.Priest;

namespace eparafia.Parafia;

public class Parafia
{
    public Parafia(int id, string name, string city, string address, List<Priest.Priest> priests, string createdDate, string subscriptionExpiration, decimal subscriptionPrice)
    {
        Id = id;
        Name = name;
        City = city;
        Address = address;
        Priests = priests;
        CreatedDate = createdDate;
        SubscriptionExpiration = subscriptionExpiration;
        SubscriptionPrice = subscriptionPrice;
    }

    public int Id { get; }
    public string Name { get; }
    public string City { get; }
    public string Address { get; }
    public List<Priest.Priest> Priests { get; }
    public string CreatedDate { get; }
    public string SubscriptionExpiration { get; }
    public decimal SubscriptionPrice { get; }
    
}