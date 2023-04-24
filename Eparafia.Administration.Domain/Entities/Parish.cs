using Eparafia.Domain.Objects;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.Entities;

public class Parish : Entity
{
    public string? CallName { get; set; }
    public ICollection<Priest> Priests { get; set; }
    public ICollection<BaptismRegister> BaptismRegisters { get; set; }
}