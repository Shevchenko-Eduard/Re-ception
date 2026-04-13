using Domain.Entity.User.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User.Permission;

public class PermissionActionConfig : IEntityTypeConfiguration<PermissionAction>
{
    public void Configure(EntityTypeBuilder<PermissionAction> builder)
    {
        #region table
        builder.ToTable("permission_actions");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(pa => pa.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        #endregion

        #region fk
        builder.HasMany(pa => pa.Permissions)
            .WithOne(p => p.PermissionAction)
            .HasForeignKey(p => p.ActionId);
        #endregion

        #region ignore
        builder.Ignore(pa => pa.Parents);
        #endregion

        #region index
        // Поиск по названию действия
        builder.HasIndex(pa => pa.Name)
            .HasDatabaseName("ix_permission_actions_name")
            .IsUnique();
        #endregion
    }
}
