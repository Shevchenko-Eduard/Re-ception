using Infrastructure.Database.Converter;
using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public partial class ProgramContext(IConnectionStrategy connectionStrategy) : DbContext
{
    private readonly IConnectionStrategy _connectionStrategy = connectionStrategy;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _connectionStrategy.Configure(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProgramContext).Assembly);

        // Глобальный конвертер
        var entityTypes = modelBuilder.Model.GetEntityTypes();
        FactoryConverter.UseConverter(entityTypes);

        base.OnModelCreating(modelBuilder);
    }
}