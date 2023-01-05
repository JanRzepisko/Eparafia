using Eparafia.Application;
using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Repository;
using Eparafia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eparafia.Infrastructure.DataAccess;

public sealed class DataContext: DbContext, IUnitOfWork
{
    private DbSet<User>? _Users { get; set; }
    private DbSet<Parish> _Parishes { get; set; }
    private DbSet<Priest> _Priests { get; set; }
    private DbSet<Announcement> _Announcement { get; set; }
    private DbSet<AnnouncementsRecords> _AnnouncementsRecords { get; set; }
    
    public IUserRepository<User> Users => new UserRepository<User>(_Users);
    public IPriestRepository Priests => new PriestRepository(_Priests);
    public IParishRepository Parishes => new ParishRepository(_Parishes);
    public IAnnouncementRepository Announcements => new AnnouncementRepository(_Announcement);
    public IAnnouncementRecordRepository AnnouncementsRecords => new AnnouncementRecordRepository(_AnnouncementsRecords);
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //I don't know why it only works here xd
        modelBuilder.Entity<Parish>().OwnsOne(x => x.Address);
        modelBuilder.Entity<Parish>().OwnsOne(x => x.Contact);
        modelBuilder.Entity<Priest>().OwnsOne(x => x.Contact);
    }
}
//create migration use this
//dotnet ef migrations add init3 --project ..\Eparafia.Infrastructure\Eparafia.Infrastructure.csproj
//in API project
   