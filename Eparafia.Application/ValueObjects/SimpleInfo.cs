namespace Eparafia.Application.ValueObjects;

public abstract class SimpleInfo
{
    public SimpleInfo(string email, string phone, string address, string city)
    {
        Email = email;
        Phone = phone;
        Address = address;
        City = city;
    }

    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
}