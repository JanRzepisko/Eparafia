using Eparafia.Administration.Domain.DefaultModel;
using Eparafia.Administration.Domain.ValueObjects;

namespace Eparafia.Administration.Domain.Entities;

public class WeddingRegister : RegisterModel
{
    public Guid Bride { get; set; }
    public Guid Groom { get; set; }
}