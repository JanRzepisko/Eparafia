using Eparafia.Application.Repository;
using Eparafia.Application.Services.FileManager;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Domain.Entities;
using Eparafia.Identity.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eparafia.Identity.Infrastructure.DataAccess;

public class DataContext : DbContext, IUnitOfWork 
{
    public DbSet<User> _Users { get; set; }
    public DbSet<Priest> _Priests { get; set; }

    public IUserRepository<User> Users => new UserRepository<User>(_Users);
    public IUserRepository<Priest> Priests => new UserRepository<Priest>(_Priests);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Priest>().OwnsOne(x => x.PhotoPath);
        modelBuilder.Entity<Priest>().OwnsOne(x => x.Contact);
        //modelBuilder.Entity<User>().OwnsOne(x => x.PhotoPath);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    
}