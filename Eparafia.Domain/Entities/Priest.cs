using Eparafia.Application.Enums;
using Eparafia.Domain.Enums;
using Eparafia.Domain.Objects;
using Eparafia.Domain.ValueObjects;

namespace Eparafia.Domain.Entities;

public class Priest : UserModel
{
    public FunctionParish FunctionParish { get; set; }
    public ICollection<Announcement> CreatedAnnouncements { get; set; }
}