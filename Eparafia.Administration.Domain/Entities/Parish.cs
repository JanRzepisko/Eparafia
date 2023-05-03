using Eparafia.Administration.Domain.Entities.ParishRecord;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.Entities;

public class Parish : Entity
{
    public string? CallName { get; set; }
    public ICollection<Priest> Priests { get; set; }
    public ICollection<BaptismRegister> BaptismRegisters { get; set; }
    public ICollection<WeddingRegister> WeddingRegisters { get; set; }
    public ICollection<DeadRegister> DeadRegisters { get; set; }
    public ICollection<HomeRecord> HomeRecords { get; set; }
}