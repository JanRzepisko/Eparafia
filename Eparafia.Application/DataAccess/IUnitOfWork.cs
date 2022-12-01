using Eparafia.Application.Entities;
using Eparafia.Application.Repository;

namespace Eparafia.Application.DataAccess;

public interface IUnitOfWork
{
    public IBaseRepository<User> Users { get; }
    //public IBaseRepository<Priest> Priests { get; }
    //public IBaseRepository<Parish> Parishes { get; }
}