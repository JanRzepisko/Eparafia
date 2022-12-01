using System.Reflection;
using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Repository;
using Eparafia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Infrastructure.DataAccess;

public class DataContext: DbContext, IUnitOfWork
{
    private DbSet<User>? _Users { get; set; }
    //private DbSet<Parish> _Parishes { get; set; }
    //private DbSet<Priest> _Priests { get; set; }
    
    public IBaseRepository<User> Users => new BaseRepository<User>(_Users);
    //public IBaseRepository<Priest> Priests => new BaseRepository<Priest>(_Priests);
    //public IBaseRepository<Parish> Parishes => new BaseRepository<Parish>(_Parishes);


    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DataContext() : base() { }
}

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseNpgsql();
        return new DataContext(optionsBuilder.Options);
    }
}