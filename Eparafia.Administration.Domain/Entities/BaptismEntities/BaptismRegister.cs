using Eparafia.Administration.Domain.DefaultModel;
using Eparafia.Administration.Domain.Entities.BaptismEntities;
using Eparafia.Administration.Domain.Entities.BaptismParents;
using Eparafia.Administration.Domain.Enums;
using Eparafia.Administration.Domain.ValueObjects;

namespace Eparafia.Administration.Domain.Entities;

public class BaptismRegister : RegisterModel
{
    public ActId ActId { get; set; }
    
    public Guid ClientId { get; set; }
    public BaptismClient Client { get; set; }
    public Guid ParentsId { get; set; }
    public BaptismParentsRelation Parents { get; set; }
    
    public Guid GodmotherId { get; set; }
    public BaptismGodmother Godmother { get; set; }
    
    public Guid GodfatherId { get; set; }
    public BaptismGodfather Godfather { get; set; }
}