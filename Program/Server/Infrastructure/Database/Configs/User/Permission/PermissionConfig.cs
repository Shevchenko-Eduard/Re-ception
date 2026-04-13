using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User.Permission;

public class PermissionConfig : IEntityTypeConfiguration<Domain.Entity.User.Permission.Permission>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.User.Permission.Permission> builder)
    {
        #region table
        builder.ToTable("permissions");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(p => p.ActionId)
            .HasColumnName("action_id")
            .IsRequired();

        builder.Property(p => p.EntityId)
            .HasColumnName("entity_id")
            .IsRequired();

        builder.Property(p => p.FlagId)
            .HasColumnName("flag_id")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(p => p.PermissionAction)
            .WithMany(pa => pa.Permissions)
            .HasForeignKey(p => p.ActionId)
            .IsRequired();

        builder.HasOne(p => p.PermissionEntity)
            .WithMany(pe => pe.Permissions)
            .HasForeignKey(p => p.EntityId)
            .IsRequired();

        builder.HasOne(p => p.PermissionFlag)
            .WithMany(pf => pf.Permissions)
            .HasForeignKey(p => p.FlagId)
            .IsRequired();

        builder.HasMany(p => p.UserPermissions)
            .WithOne(up => up.Permission)
            .HasForeignKey(up => up.PermissionId);

        builder.HasMany(p => p.Roles)
            .WithMany(r => r.Permissions)
            .UsingEntity("role_permissions",
                l => l.HasOne(typeof(Domain.Entity.User.Role.Role)).WithMany().HasForeignKey("role_id"),
                r => r.HasOne(typeof(Domain.Entity.User.Permission.Permission)).WithMany().HasForeignKey("permission_id"));
        #endregion

        #region ignore
        #endregion

        #region index
        // Поиск по действию
        builder.HasIndex(p => p.ActionId)
            .HasDatabaseName("ix_permissions_action_id");

        // Поиск по сущности
        builder.HasIndex(p => p.EntityId)
            .HasDatabaseName("ix_permissions_entity_id");

        // Поиск по флагу
        builder.HasIndex(p => p.FlagId)
            .HasDatabaseName("ix_permissions_flag_id");

        // Комбинированный индекс для уникального определения разрешения
        builder.HasIndex(p => new { p.ActionId, p.EntityId, p.FlagId })
            .HasDatabaseName("ix_permissions_action_entity_flag")
            .IsUnique();
        #endregion
    }
}
