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
    
    public IUserRepository<User> Users => new UserRepository<User>(_Users);
    public IUserRepository<Priest?> Priests => new UserRepository<Priest?>(_Priests);
    public IBaseRepository<Parish> Parishes => new BaseRepository<Parish>(_Parishes);
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
}

//create migration use this
//dotnet ef migrations add init3 --project ..\Eparafia.Infrastructure\Eparafia.Infrastructure.csproj
//in API project
   