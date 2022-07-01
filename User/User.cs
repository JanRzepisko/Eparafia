namespace eparafia.User;

public class User
{
    public User(string name, string surName, string email, string phoneNumber, int parafia, int id, string address, bool isActive)
    {
        Name = name;
        SurName = surName;
        Email = email;
        PhoneNumber = phoneNumber;
        Parafia = parafia;
        Id = id;
        Address = address;
        IsActive = isActive;
    }
    
    public int Id { get; }
    public string Name { get; }
    public string SurName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public int Parafia { get; }
    public string Address { get; }
    public bool IsActive { get; }
}