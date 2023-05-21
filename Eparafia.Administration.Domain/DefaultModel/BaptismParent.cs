using Eparafia.Administration.Domain.Enums;
using Eparafia.Domain.ValueObjects;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.DefaultModel;

public class BaptismParent : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string CityOfBirth { get; set; }
    public Confession Confession { get; set; }
    public string Job { get; set; }
    public Address Address { get; set; }
}