using Eparafia.Bible.Application.DataAccess;
using Eparafia.Bible.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Bible.Infrastructure.DataAccess;

public class DataContext : DbContext, IUnitOfWork
{
    public DataContext(DbContextOptions<DataContext> options) : base(options){}
    
    private DbSet<Day> _Days { get; set; }
    private DbSet<Reading> _Readings { get; set; }

    public IBaseRepository<Day> Days => new BaseRepository<Day>(_Days);
    public IBaseRepository<Reading> Readings => new BaseRepository<Reading>(_Readings);
}
//create migration use this
//dotnet ef migrations add init --project ..\Eparafia.Bible.Infrastructure\Eparafia.Bible.Infrastructure.csproj
//dotnet ef database update -- --environment Development
//in API project
