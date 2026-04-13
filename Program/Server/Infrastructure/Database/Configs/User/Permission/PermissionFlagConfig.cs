using Domain.Entity.User.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User.Permission;

public class PermissionFlagConfig : IEntityTypeConfiguration<PermissionFlag>
{
    public void Configure(EntityTypeBuilder<PermissionFlag> builder)
    {
        #region table
        builder.ToTable("permission_flags");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(pf => pf.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        #endregion

        #region fk
        builder.HasMany(pf => pf.Permissions)
            .WithOne(p => p.PermissionFlag)
            .HasForeignKey(p => p.FlagId);
        #endregion

        #region ignore
        builder.Ignore(pf => pf.Parents);
        #endregion

        #region index
        // Поиск по названию флага
        builder.HasIndex(pf => pf.Name)
            .HasDatabaseName("ix_permission_flags_name")
            .IsUnique();
        #endregion
    }
}
