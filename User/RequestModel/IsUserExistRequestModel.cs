using eparafia.Helpers;

namespace eparafia.Models;

public class IsUserExistRequestModel : Request
{
    public IsUserExistRequestModel(int id, string token) : base(token)
    {
        Id = id;
    }

    public int Id { get; }
}