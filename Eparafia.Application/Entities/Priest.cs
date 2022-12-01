using Eparafia.Application.DataAccess.Abstract;

namespace Eparafia.Application.Entities;

public class Priest : Entity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    
    public bool IsActive { get; set; }
    public bool HasAvatar { get; set; }
    
    public Guid ParishId { get; set; }
    public Parish? Parish { get; set; }
}