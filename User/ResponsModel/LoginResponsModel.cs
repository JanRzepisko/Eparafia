namespace eparafia.Models.ResponsModel;

public class LoginResponsModel
{
    public LoginResponsModel(User.User user, string token)
    {
        Name = user.Name;
        SurName = user.SurName;
        Email = user.Email;
        PhoneNumber = user.PhoneNumber;
        Parafia = user.Parafia;
        Id = user.Id;
        Adress = user.Address;
        IsActive = user.IsActive;
        Token = token;
    }
    
    public int Id { get; }
    public string Name { get; }
    public string SurName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public int Parafia { get; }
    public string Adress { get; }
    public bool IsActive { get; }
    public string Token { get; }
}