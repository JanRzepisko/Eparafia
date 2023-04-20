using Eparafia.Administration.Application.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Eparafia.Administration.Infrastructure.DataAccess;

public class DataContext : DbContext, IUnitOfWork
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}