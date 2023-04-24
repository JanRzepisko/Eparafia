using Eparafia.Administration.Application.DataAccess;
using Eparafia.Administration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Infrastructure.DataAccess;

public sealed class DataContext : DbContext, IUnitOfWork
{
    private DbSet<BaptismRegister> _baptismRegister { get; set; }
    private DbSet<WeddingRegister> _weddingRegister { get; set; }
    private DbSet<DeadRegister> _deadRegister { get; set; }
    private DbSet<Parish> _parish { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //I don't know why, but it only works here 
        modelBuilder.Entity<BaptismRegister>().OwnsOne(x => x.Client);
        modelBuilder.Entity<BaptismRegister>().OwnsOne(x => x.Father);
        modelBuilder.Entity<BaptismRegister>().OwnsOne(x => x.Mother);
        modelBuilder.Entity<BaptismRegister>().OwnsOne(x => x.Godfather);
        modelBuilder.Entity<BaptismRegister>().OwnsOne(x => x.Godmother);
        modelBuilder.Entity<BaptismRegister>().OwnsOne(x => x.ActId);
        modelBuilder.Entity<BaptismRegister>().OwnsOne(x => x.SacramentalApprove);

        modelBuilder.Entity<WeddingRegister>().OwnsOne(x => x.Bride);
        modelBuilder.Entity<WeddingRegister>().OwnsOne(x => x.Groom);
        modelBuilder.Entity<DeadRegister>().OwnsOne(x => x.Client);
    }

    public IBaseRepository<BaptismRegister> BaptismRegister => new BaseRepository<BaptismRegister>(_baptismRegister);
    public IBaseRepository<WeddingRegister> WeddingRegister => new BaseRepository<WeddingRegister>(_weddingRegister);
    public IBaseRepository<DeadRegister> DeadRegister => new BaseRepository<DeadRegister>(_deadRegister);
    public IBaseRepository<Parish> Parish => new BaseRepository<Parish>(_parish);
}