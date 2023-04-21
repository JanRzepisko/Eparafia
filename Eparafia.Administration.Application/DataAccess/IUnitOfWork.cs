using Eparafia.Administration.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Application.DataAccess;

public interface IUnitOfWork
{ 
    IBaseRepository<BaptismRegister> BaptismRegister { get; }
    IBaseRepository<WeddingRegister> WeddingRegister { get; }
    IBaseRepository<DeadRegister> DeadRegister { get; }
    IBaseRepository<Parish> Parish { get; }
}