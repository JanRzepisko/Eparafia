using Eparafia.Domain.Objects;
using Eparafia.Identity.Domain.ValueObjects;

namespace Eparafia.Identity.Domain.Entities;

public class Priest : UserModel
{
    public Contact Contact { get; set; }
}