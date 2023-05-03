using Eparafia.Administration.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.ValueObjects;

public class BaptismClient : Entity
{
    public BaptismRegister BaptismRegister { get; set; }
    public Guid BaptismRegisterId { get; set; }

    public string FirstName { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly BaptismDate { get; set; }
}