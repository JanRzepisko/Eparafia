using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.Entities;

public class DeadClient : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly DeathDate { get; set; }
    
    public DeadRegister DeadRegister { get; set; }
    public Guid DeadRegisterId { get; set; }
}