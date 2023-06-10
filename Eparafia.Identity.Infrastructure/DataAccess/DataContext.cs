using Eparafia.Application.Repository;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Domain.Entities;
using Eparafia.Identity.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;


namespace Eparafia.Identity.Infrastructure.DataAccess;

public class DataContext : DbContext, IUnitOfWork
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    private DbSet<User> _Users { get; set; }
    private DbSet<Priest> _Priests { get; set; }
    private DbSet<UserSession> _UserSessions { get; set; }

    public IUserRepository<User> Users => new UserRepository<User>(_Users);
    public IUserRepository<Priest> Priests => new UserRepository<Priest>(_Priests);
    public IBaseRepository<UserSession> UserSessions => new BaseRepository<UserSession>(_UserSessions);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Priest>().OwnsOne(x => x.PhotoPath);
        modelBuilder.Entity<Priest>().OwnsOne(x => x.Contact);
        //modelBuilder.Entity<User>().OwnsOne(x => x.PhotoPath);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}