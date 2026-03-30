using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database;

public class AppContext : DbContext
{
    private readonly IConnectionStrategy _connectionStrategy;
    public AppContext(IConnectionStrategy connectionStrategy)
    {
        _connectionStrategy = connectionStrategy;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _connectionStrategy.Configure(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Глобальный конвертер для всех Guid свойств
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(Guid))
                {
                    property.SetValueConverter(new GuidToStringConverter());
                    property.SetMaxLength(36);
                }
            }
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}