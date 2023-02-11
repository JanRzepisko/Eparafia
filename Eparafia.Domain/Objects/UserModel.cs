using Eparafia.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Domain.Objects;

public class UserModel : Entity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string? Role { get; set; }
    public bool IsActive { get; set; }
    public Guid? ParishId { get; set; }
    public Parish Parish { get; set; }
    public string PasswordHash { get; set; }

    public string PhotoPath { get; set; }
    public string PhotoPathMin { get; set; }

}