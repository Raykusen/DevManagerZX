using Microsoft.EntityFrameworkCore;
using DevManager.Data.Models;

namespace DevManager.Data.Context;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class AppDbContext : DbContext, IAppDbContext
{
    #region Constructor
    private readonly IConfiguration config;

    public AppDbContext(IConfiguration config)
    {
        this.config = config;
    }
    #endregion

    #region Tablas
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    #endregion

    #region Funciones
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("MSSQL"));
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
