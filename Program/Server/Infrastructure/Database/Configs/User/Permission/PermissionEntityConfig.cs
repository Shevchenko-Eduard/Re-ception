using Domain.Entity.User.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User.Permission;

public class PermissionEntityConfig : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        #region table
        builder.ToTable("permission_entities");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(pe => pe.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        #endregion

        #region fk
        builder.HasMany(pe => pe.Permissions)
            .WithOne(p => p.PermissionEntity)
            .HasForeignKey(p => p.EntityId);
        #endregion

        #region ignore
        builder.Ignore(pe => pe.Parents);
        #endregion

        #region index
        // Поиск по названию сущности
        builder.HasIndex(pe => pe.Name)
            .HasDatabaseName("ix_permission_entities_name")
            .IsUnique();

        #endregion
    }
}
