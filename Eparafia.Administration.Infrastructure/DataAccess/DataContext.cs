using Eparafia.Administration.Application.DataAccess;
using Eparafia.Administration.Domain.Entities;
using Eparafia.Administration.Domain.Entities.BaptismEntities;
using Eparafia.Administration.Domain.Entities.Dead;
using Eparafia.Administration.Domain.Entities.ParishRecord;
using Eparafia.Administration.Domain.Entities.WeddingEntities;
using Eparafia.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Infrastructure.DataAccess;

public sealed class DataContext : DbContext, IUnitOfWork
{
    private DbSet<Parish> _parish { get; set; }
    private DbSet<Priest> _priest { get; set; }
    private DbSet<SacramentalMaker> _sacramentalMaker { get; set; }
    private DbSet<BaptismClient> _baptismClient { get; set; }
    private DbSet<BaptismFather> _baptismFather { get; set; }
    private DbSet<BaptismMother> _baptismMother { get; set; }
    private DbSet<BaptismGodfather> _baptismGodfather { get; set; }
    private DbSet<BaptismGodmother> _baptismGodmother { get; set; }
    private DbSet<BaptismRegister> _baptismRegister { get; set; }
    private DbSet<BaptismParentsRelation> _baptismParentsRelation { get; set; }
    private DbSet<Men> _weddingMen { get; set; }
    private DbSet<Women> _weddingWomen { get; set; }
    private DbSet<Witness> _weddingWitness { get; set; }
    private DbSet<WeddingRegister> _weddingRegister { get; set; }
    private DbSet<DeadClient> _deadClient { get; set; }
    private DbSet<DeadRegister> _deadRegister { get; set; }
    private DbSet<HomeRecord> _homeRecord { get; set; }
    private DbSet<Person> _person { get; set; }



    public IBaseRepository<BaptismRegister> BaptismRegister => new BaseRepository<BaptismRegister>(_baptismRegister);
    public IBaseRepository<WeddingRegister> WeddingRegister => new BaseRepository<WeddingRegister>(_weddingRegister);
    public IBaseRepository<DeadRegister> DeadRegister => new BaseRepository<DeadRegister>(_deadRegister);
    public IBaseRepository<Parish> Parish => new BaseRepository<Parish>(_parish);
    public IBaseRepository<Priest> Priests => new BaseRepository<Priest>(_priest);
    public IBaseRepository<SacramentalMaker> SacramentalMaker => new BaseRepository<SacramentalMaker>(_sacramentalMaker);
    public IBaseRepository<BaptismClient> BaptismClient => new BaseRepository<BaptismClient>(_baptismClient);
    public IBaseRepository<BaptismFather> BaptismFather => new BaseRepository<BaptismFather>(_baptismFather);
    public IBaseRepository<BaptismMother> BaptismMother => new BaseRepository<BaptismMother>(_baptismMother);
    public IBaseRepository<BaptismGodfather> BaptismGodfather => new BaseRepository<BaptismGodfather>(_baptismGodfather);
    public IBaseRepository<BaptismGodmother> BaptismGodmother => new BaseRepository<BaptismGodmother>(_baptismGodmother);
    public IBaseRepository<BaptismParentsRelation> BaptismParentsRelation => new BaseRepository<BaptismParentsRelation>(_baptismParentsRelation);
    public IBaseRepository<Men> WeddingMen => new BaseRepository<Men>(_weddingMen);
    public IBaseRepository<Women> WeddingWomen => new BaseRepository<Women>(_weddingWomen);
    public IBaseRepository<Witness> WeddingWitness => new BaseRepository<Witness>(_weddingWitness);
    public IBaseRepository<DeadClient> DeadClient => new BaseRepository<DeadClient>(_deadClient);
    public IBaseRepository<HomeRecord> HomeRecord => new BaseRepository<HomeRecord>(_homeRecord);
    public IBaseRepository<Person> Person => new BaseRepository<Person>(_person);

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //I don't know why, but it only works here 
        modelBuilder.Entity<BaptismClient>().HasOne(c => c.BaptismRegister).WithOne(c => c.Client)
            .HasForeignKey<BaptismClient>(c => c.BaptismRegisterId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<BaptismFather>().HasOne(c => c.BaptismParentsRelation).WithOne(c => c.Father)
            .HasForeignKey<BaptismFather>(c => c.BaptismParentsRelationId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<BaptismGodfather>().HasOne(c => c.BaptismRegister).WithOne(c => c.Godfather)
            .HasForeignKey<BaptismGodfather>(c => c.BaptismRegisterId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<BaptismGodmother>().HasOne(c => c.BaptismRegister).WithOne(c => c.Godmother)
            .HasForeignKey<BaptismGodmother>(c => c.BaptismRegisterId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<BaptismMother>().HasOne(c => c.BaptismParentsRelation).WithOne(c => c.Mother)
            .HasForeignKey<BaptismMother>(c => c.BaptismParentsRelationId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<BaptismParentsRelation>().HasOne(c => c.BaptismRegister).WithOne(c => c.Parents)
            .HasForeignKey<BaptismParentsRelation>(c => c.BaptismRegisterId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<DeadClient>().HasOne(c => c.DeadRegister).WithOne(c => c.Client)
            .HasForeignKey<DeadClient>(c => c.DeadRegisterId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Men>().HasOne(c => c.WeddingRegister).WithOne(c => c.Men)
            .HasForeignKey<Men>(c => c.WeddingRegisterId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Women>().HasOne(c => c.WeddingRegister).WithOne(c => c.Women)
            .HasForeignKey<Women>(c => c.WeddingRegisterId).OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<HomeRecord>().OwnsOne<Address>(c => c.Address);
        modelBuilder.Entity<BaptismFather>().OwnsOne<Address>(c => c.Address);
        modelBuilder.Entity<BaptismMother>().OwnsOne<Address>(c => c.Address);
        modelBuilder.Entity<BaptismGodfather>().OwnsOne<Address>(c => c.Address);
        modelBuilder.Entity<BaptismGodmother>().OwnsOne<Address>(c => c.Address);
    }
}