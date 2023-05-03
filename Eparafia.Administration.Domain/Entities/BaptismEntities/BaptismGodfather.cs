using Eparafia.Administration.Domain.DefaultModel;

namespace Eparafia.Administration.Domain.Entities.BaptismParents;

public class BaptismGodfather : BaptismParent
{
    public Guid BaptismRegisterId { get; set; }
    public BaptismRegister BaptismRegister { get; set; }
}