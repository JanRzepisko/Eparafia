using Eparafia.Application.DataAccess.Abstract;

namespace Eparafia.Application.Entities;

public class Parish : Entity
{
    public string? CallName { get; set; }
    public Address Address { get; set; }
    public Contact Contact { get; set; }
    public ICollection<User>? Users { get; set; }
    public ICollection<Priest>? Priests { get; set; }
    public ICollection<Announcement>? Announcements { get; set; }
    public ICollection<Post>? Posts { get; set; }
}