using Eparafia.Administration.Domain.Entities.BaptismEntities;
using Eparafia.Administration.Domain.Entities.Dead;
using Eparafia.Administration.Domain.Entities.WeddingEntities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.Entities;

public class SacramentalMaker : Entity
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    
    public ICollection<BaptismRegister> BaptismRegisters { get; set; }
    public ICollection<WeddingRegister> WeddingRegisters { get; set; }
    public ICollection<DeadRegister> DeadRegisters { get; set; }
}