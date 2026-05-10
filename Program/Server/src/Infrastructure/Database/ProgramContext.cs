using Infrastructure.Database.Converter;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public partial class ProgramContext : DbContext
{
    private readonly Guid _instanceId = Guid.NewGuid();

    public ProgramContext(DbContextOptions<ProgramContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine($"ProgramContext created: {_instanceId}");
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