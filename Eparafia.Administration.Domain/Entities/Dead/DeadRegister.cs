using Eparafia.Administration.Domain.DefaultModel;

namespace Eparafia.Administration.Domain.Entities.Dead;

public class DeadRegister : RegisterModel
{
    public Guid ClientId { get; set; }
    public DeadClient Client { get; set; }
}