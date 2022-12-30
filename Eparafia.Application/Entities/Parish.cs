using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.ValueObjects;

namespace Eparafia.Application.Entities;

public class Parish : Entity
{
    public string? Name { get; set; }
    public SimpleInfo ParishInfo { get; set; }
    public ICollection<User>? Users { get; set; }
    public ICollection<Priest>? Priests { get; set; }
}