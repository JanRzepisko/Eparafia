namespace eparafia.Parafia.RequestModel;
public class PriestParafiaRegister
{
    public PriestParafiaRegister(string name, string surName, string email, string phoneNumber)
    {
        Name = name;
        SurName = surName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string Name { get; }
    public string SurName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
}