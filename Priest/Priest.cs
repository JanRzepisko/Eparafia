namespace eparafia.Priest;

public class Priest
{
    public Priest(PriestRole role, string phoneNumber, int yearOfOrdination, string surName, string email, string name, int id, int parafia)
    {
        Role = role;
        PhoneNumber = phoneNumber;
        YearOfOrdination = yearOfOrdination;
        SurName = surName;
        Email = email;
        Name = name;
        Id = id;
        Parafia = parafia;
    }

    public int Id { get; }
    public string Name { get; }
    public string SurName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }   
    public int YearOfOrdination { get; }
    public PriestRole Role { get; }
    public int Parafia { get; }
}