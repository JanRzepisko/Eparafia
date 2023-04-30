using Eparafia.Administration.Domain.DefaultModel;
using Eparafia.Administration.Domain.ValueObjects;

namespace Eparafia.Administration.Domain.Entities.BaptismParents;

public class BaptismGodfather : BaptismGodparent
{
    public Guid BaptismRegisterId { get; set; }
    public BaptismRegister BaptismRegister { get; set; }
}