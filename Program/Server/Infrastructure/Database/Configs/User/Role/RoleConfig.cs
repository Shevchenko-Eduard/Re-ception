using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User.Role;

public class RoleConfig : IEntityTypeConfiguration<Domain.Entity.User.Role.Role>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.User.Role.Role> builder)
    {
        #region table
        builder.ToTable("roles");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(r => r.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(r => r.Description)
            .HasColumnName("description")
            .HasMaxLength(100)
            .IsRequired(false);
        #endregion

        #region fk
        builder.HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId);

        builder.HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity("role_permissions",
                l => l.HasOne(typeof(Domain.Entity.User.Permission.Permission)).WithMany().HasForeignKey("permission_id"),
                r => r.HasOne(typeof(Domain.Entity.User.Role.Role)).WithMany().HasForeignKey("role_id"));
        #endregion

        #region ignore
        #endregion

        #region index
        // Поиск по названию роли
        builder.HasIndex(r => r.Name)
            .HasDatabaseName("ix_roles_name")
            .IsUnique();
        #endregion
    }
}