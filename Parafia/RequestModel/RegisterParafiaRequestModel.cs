using eparafia.Calendar.Event;
using eparafia.Helpers;

namespace eparafia.Parafia.RequestModel;

public class RegisterParafiaRequestModel : Request
{
    public RegisterParafiaRequestModel(string name, string city, string address, string subscriptionExpiration, decimal subscriptionPrice, List<PriestParafiaRegister> priests, string contactPhoneNumber, List<DefaultEvent> defaultWeek)
    {
        Name = name;
        City = city;
        Address = address;
        SubscriptionExpiration = subscriptionExpiration;
        SubscriptionPrice = subscriptionPrice;
        Priests = priests;
        ContactPhoneNumber = contactPhoneNumber;
        DefaultWeek = defaultWeek;
    }
    public string Name { get; }
    public string City { get; }
    public string Address { get; }
    public string SubscriptionExpiration { get; }
    public decimal SubscriptionPrice { get; }
    public List<PriestParafiaRegister> Priests { get; }
    public string ContactPhoneNumber { get; }
    public List<DefaultEvent> DefaultWeek { get; }
}