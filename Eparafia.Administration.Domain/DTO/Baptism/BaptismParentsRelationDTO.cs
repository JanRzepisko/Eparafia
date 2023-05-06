using Eparafia.Administration.Domain.Enums;

namespace Eparafia.Administration.Domain.DTO.Baptism;

public class BaptismParentsRelationDTO
{
    public ParentRelation ParentRelation { get; set; }
    public BaptismParentDTO Father { get; set; }
    public BaptismParentDTO Mother { get; set; }
}