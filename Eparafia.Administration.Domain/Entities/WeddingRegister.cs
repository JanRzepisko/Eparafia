using Eparafia.Administration.Domain.DefaultModel;
using Eparafia.Administration.Domain.ValueObjects;

namespace Eparafia.Administration.Domain.Entities;

public class WeddingRegister : RegisterModel
{
    public Person Bride { get; set; }
    public Person Groom { get; set; }
}