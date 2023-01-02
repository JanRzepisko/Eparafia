using System.Runtime.CompilerServices;
using Eparafia.Application.Services.FileManager;

namespace Eparafia.Application.DataAccess.Abstract;

public class UserModel : Entity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string? Role { get; set; }
    public bool IsActive { get; set; }
    public Guid? ParishId { get; set; }
    public Entities.Parish Parish { get; set; }
    public string PasswordHash { get; set; }

    public string PhotoPath { get; set; }
    public string PhotoPathMin { get; set; }

}