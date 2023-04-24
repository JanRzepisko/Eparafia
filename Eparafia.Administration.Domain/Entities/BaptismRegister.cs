using Eparafia.Administration.Domain.DefaultModel;
using Eparafia.Administration.Domain.Enums;
using Eparafia.Administration.Domain.ValueObjects;

namespace Eparafia.Administration.Domain.Entities;

public class BaptismRegister : RegisterModel
{
    public Guid ParishId { get; set; }
    public Parish Parish { get; set; }
    public Guid SacramentalMakerId { get; set; }
    public SacramentalMaker SacramentalMaker { get; set; }
    public ActId ActId { get; set; }
    public BaptismClient Client { get; set; }
    public BaptismParent Mother { get; set; }
    public BaptismParent Father { get; set; }
    public ParentRelation ParentRelation { get; set; }
    public BaptismParent Godmother { get; set; }
    public BaptismParent Godfather { get; set; }
    public SacramentalApprove SacramentalApprove { get; set; }
    public string Comments { get; set; }
}