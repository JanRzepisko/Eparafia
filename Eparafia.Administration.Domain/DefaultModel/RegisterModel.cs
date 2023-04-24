using Eparafia.Administration.Domain.ValueObjects;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.DefaultModel;

public class RegisterModel : Entity
{
    public DateTime DateOfSacrament { get; set; }
    public string Comments { get; set; }
    public BaptismParent Priest { get; set; }
}