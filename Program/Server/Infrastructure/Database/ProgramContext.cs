using Domain.Entity;
using Infrastructure.Database.Converter;
using Infrastructure.Database.IdentityEntity;
using Infrastructure.Database.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using GuidToStringConverter = Infrastructure.Database.Converter.GuidToStringConverter;

namespace Infrastructure.Database;

public partial class ProgramContext(IConnectionStrategy connectionStrategy) : IdentityUserContext<ApplicationUser>
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