using Eparafia.Application.DataAccess.Abstract;

namespace Eparafia.Application.Entities;

public class Parish : Entity
{
    public string? Name { get; set; }
    public string? City { get; set; }
    public ICollection<User>? Users { get; set; }
    public ICollection<Priest>? Priests { get; set; }
}