using Eparafia.Application.DataAccess.Abstract;

namespace Eparafia.Application.Entities;

public class Parish : Entity
{
    public string? Name { get; set; }
    ICollection<User>? Users { get; set; }
    ICollection<Priest>? Priests { get; set; }
}