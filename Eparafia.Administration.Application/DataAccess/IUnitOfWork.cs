using Eparafia.Administration.Domain.Entities;
using Eparafia.Administration.Domain.Entities.BaptismEntities;
using Eparafia.Administration.Domain.Entities.Dead;
using Eparafia.Administration.Domain.Entities.ParishRecord;
using Eparafia.Administration.Domain.Entities.WeddingEntities;
using Shared.BaseModels;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Application.DataAccess;

public interface IUnitOfWork
{ 
    IBaseRepository<BaptismRegister> BaptismRegister { get; }
    IBaseRepository<WeddingRegister> WeddingRegister { get; }
    IBaseRepository<DeadRegister> DeadRegister { get; }
    IBaseRepository<Parish> Parish { get; }
    IBaseRepository<Priest> Priests { get; }
    public IBaseRepository<SacramentalMaker> SacramentalMaker { get; }
    public IBaseRepository<BaptismClient> BaptismClient  { get; }
    public IBaseRepository<BaptismFather> BaptismFather  { get; }
    public IBaseRepository<BaptismMother> BaptismMother { get; }
    public IBaseRepository<BaptismGodfather> BaptismGodfather  { get; }
    public IBaseRepository<BaptismGodmother> BaptismGodmother  { get; }
    public IBaseRepository<BaptismParentsRelation> BaptismParentsRelation  { get; }
    public IBaseRepository<Men> WeddingMen  { get; }
    public IBaseRepository<Women> WeddingWomen  { get; }
    public IBaseRepository<Witness> WeddingWitness { get; }
    public IBaseRepository<DeadClient> DeadClient { get; }
    public IBaseRepository<HomeRecord> HomeRecord { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}