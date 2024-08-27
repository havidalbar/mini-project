using Microsoft.EntityFrameworkCore;
using Persistence.EntitiesConfiguration;
using Persistence.Models;

namespace Persistence.DatabaseContext;

public class TableContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<TodoDetail> TodoDetails { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public TableContext(DbContextOptions<TableContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}