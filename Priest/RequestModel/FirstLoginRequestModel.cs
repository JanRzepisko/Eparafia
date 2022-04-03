using eparafia.Helpers;

namespace eparafia.Priest;

public class FirstLoginRequestModel : Request
{
     public FirstLoginRequestModel(int yearOfOrdination, string password, string email, string fristLoginToken)
     {
          YearOfOrdination = yearOfOrdination;
          Password = password;
          Email = email;
          FristLoginToken = fristLoginToken;
     }
     
     public int YearOfOrdination { get; }
     public string Password { get; }
     public string Email { get; }
     public string FristLoginToken { get; }
}