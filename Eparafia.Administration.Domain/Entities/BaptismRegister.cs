using Eparafia.Administration.Domain.DefaultModel;
using Eparafia.Administration.Domain.ValueObjects;

namespace Eparafia.Administration.Domain.Entities;

public class BaptismRegister : RegisterModel
{
    public Person Client { get; set; }
    public Person Mother { get; set; }
    public Person Father { get; set; }
    public Person Godfather { get; set; }
    public Person Godmother { get; set; }
}