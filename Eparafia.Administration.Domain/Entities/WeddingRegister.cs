using Eparafia.Administration.Domain.DefaultModel;
using Eparafia.Administration.Domain.ValueObjects;

namespace Eparafia.Administration.Domain.Entities;

public class WeddingRegister : RegisterModel
{
    public BaptismParent Bride { get; set; }
    public BaptismParent Groom { get; set; }
}