using eparafia.Priest;

namespace eparafia.Models.ResponsModel;

public class LoginPriestResponseModel
{
    public LoginPriestResponseModel(Priest.Priest priest, string token)
    {
        Name = priest.Name;
        SurName = priest.SurName;
        Email = priest.Email;
        PhoneNumber = priest.PhoneNumber;
        Parafia = priest.Parafia;
        Id = priest.Id;
        Token = token;
        YearOfOrdination = priest.YearOfOrdination;
        Role = priest.Role;
    }
    public string Token { get; }
    public int Id { get; }
    public string Name { get; }
    public string SurName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }   
    public int YearOfOrdination { get; }
    public PriestRole Role { get; }
    public int Parafia { get; }
}