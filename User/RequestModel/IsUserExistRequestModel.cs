using eparafia.Helpers;

namespace eparafia.Models;

public class IsUserExistRequestModel : Request
{
    public IsUserExistRequestModel(int id)
    {
        Id = id;
    }

    public int Id { get; }
}