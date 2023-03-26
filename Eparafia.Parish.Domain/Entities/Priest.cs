using Eparafia.Domain.Enums;
using Eparafia.Domain.Objects;

namespace Eparafia.Domain.Entities;

public class Priest : UserModel
{
    public FunctionParish FunctionParish { get; set; }
    public ICollection<Announcement> CreatedAnnouncements { get; set; }
}