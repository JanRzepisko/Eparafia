using Eparafia.Application.DataAccess;
using Eparafia.Application.Repository;
using Eparafia.Domain.Entities;
using Eparafia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Infrastructure.DataAccess;

public sealed class DataContext : DbContext, IUnitOfWork
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    private DbSet<User>? _Users { get; set; }
    private DbSet<Parish> _Parishes { get; set; }
    private DbSet<Priest> _Priests { get; set; }
    private DbSet<Announcement> _Announcement { get; set; }
    private DbSet<AnnouncementRecord> _AnnouncementsRecords { get; set; }
    private DbSet<Post> _Post { get; set; }
    private DbSet<PostFile> _PostFile { get; set; }
    private DbSet<SpecialEvent> _SpecialEvent { get; set; }
    private DbSet<CommonEvent> _CommonWeek { get; set; }
    private DbSet<Intention> _Intention { get; set; }
    private DbSet<Payment> _Payment { get; set; }

    public IUserRepository<User> Users => new UserRepository<User>(_Users);
    public IPriestRepository Priests => new PriestRepository(_Priests);
    public IParishRepository Parishes => new ParishRepository(_Parishes);
    public IAnnouncementRepository Announcements => new AnnouncementRepository(_Announcement);

    public IAnnouncementRecordRepository AnnouncementsRecords =>
        new AnnouncementRecordRepository(_AnnouncementsRecords);

    public IPostRepository Posts => new PostRepository(_Post);
    public IBaseRepository<PostFile> PostFiles => new BaseRepository<PostFile>(_PostFile);
    public ICommonWeekRepository CommonWeek => new CommonWeekRepository(_CommonWeek);
    public ISpecialEventRepository SpecialEvents => new SpecialEventRepository(_SpecialEvent);
    public IBaseRepository<Payment> Payments => new BaseRepository<Payment>(_Payment);
    public IIntentionRepository Intentions => new IntentionRepository(_Intention);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //I don't know why it only works here xd
        modelBuilder.Entity<Parish>().OwnsOne(x => x.Address);
        modelBuilder.Entity<Parish>().OwnsOne(x => x.Contact);
        modelBuilder.Entity<Priest>().OwnsOne(x => x.PhotoPath);
        modelBuilder.Entity<User>().OwnsOne(x => x.PhotoPath);
        modelBuilder.Entity<CommonEvent>().OwnsOne(x => x.Event);
        modelBuilder.Entity<SpecialEvent>().OwnsOne(x => x.Event);
    }
}
//create migration use this
//dotnet ef migrations add init3 --project ..\Eparafia.Parish.Infrastructure\Eparafia.Parish.Infrastructure.csproj
//dotnet ef database update -- --environment Development
//in API project