using Eparafia.Domain.ValueObjects;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.Entities.ParishRecord;

public class Home : Entity
{
    public Parish Parish { get; set; }
    public Guid ParishId { get; set; }
    public Address Address { get; set; }
    public ICollection<Person> Persons { get; set; }
}