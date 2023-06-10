using Eparafia.Domain.Objects;

namespace Eparafia.Identity.Domain.Entities;

public class User : UserModel
{
    public Guid ParishId { get; set; }
}