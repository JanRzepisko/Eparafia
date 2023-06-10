using Shared.BaseModels.BaseEntities;

namespace Eparafia.Identity.Domain.Entities;

public class UserSession : Entity
{
    public Guid UserId { get; set; }
    public Guid? ParishId { get; set; }
}