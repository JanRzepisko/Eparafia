using Eparafia.Application.DataAccess.Abstract;

namespace Eparafia.Application.Entities;

public class User : UserModel
{
    public string? Role { get; set; }
    public bool IsActive { get; set; }
    public bool HasAvatar { get; set; }
    public Guid ParishId { get; set; }
    public Parish? Parish { get; set; }
}