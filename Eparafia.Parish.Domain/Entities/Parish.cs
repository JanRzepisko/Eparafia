using Eparafia.Domain.ValueObjects;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Domain.Entities;

public class Parish : Entity
{
    public string? CallName { get; set; }
    public string? ShortName { get; set; }
    public Address Address { get; set; }
    public Contact Contact { get; set; }
    public ICollection<User>? Users { get; set; }
    public ICollection<Priest>? Priests { get; set; }
    public ICollection<Announcement>? Announcements { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public ICollection<CommonEvent> CommonWeek { get; set; }
    public ICollection<SpecialEvent> SpecialEvents { get; set; }
    public ICollection<Intention> Intentions { get; set; }
    public ICollection<Payment> Payments { get; set; }
}