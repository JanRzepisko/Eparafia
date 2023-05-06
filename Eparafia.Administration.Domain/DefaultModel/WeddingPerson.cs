using Eparafia.Administration.Domain.Entities;
using Eparafia.Administration.Domain.Entities.WeddingEntities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.DefaultModel;

public class WeddingPerson : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Job { get; set; }
    
    public WeddingRegister WeddingRegister { get; set; }
    public Guid WeddingRegisterId { get; set; }
}