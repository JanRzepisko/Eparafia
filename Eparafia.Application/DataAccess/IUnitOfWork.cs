using System.Linq.Expressions;
using Eparafia.Application.Entities;
using Eparafia.Application.Repository;

namespace Eparafia.Application.DataAccess;

public interface    IUnitOfWork
{
    public IUserRepository<User> Users { get; }
    public IPriestRepository Priests { get; }
    public IParishRepository Parishes { get; }
    public IAnnouncementRepository Announcements { get; }
    public IAnnouncementRecordRepository AnnouncementsRecords { get; }
    public IPostRepository Posts { get; }
    public IBaseRepository<PostFile> PostFiles { get; }
    public ICommonWeekRepository CommonWeek { get; }
    public ISpecialEventRepository SpecialEvents { get; }
    public IIntentionRepository Intentions { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}