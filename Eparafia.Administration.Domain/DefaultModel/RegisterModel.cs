using Eparafia.Administration.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.DefaultModel;

public class RegisterModel : Entity
{
    public DateTime DateOfSacrament { get; set; }
    public string Comments { get; set; }
    public SacramentalMaker SacramentalMaker { get; set; }
    public Guid SacramentalMakerId { get; set; }
    public Parish Parish { get; set; }
    public Guid ParishId { get; set; }
}