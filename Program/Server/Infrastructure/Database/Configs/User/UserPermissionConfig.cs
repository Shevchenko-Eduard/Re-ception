using Domain.Entity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User;

public class UserPermissionConfig : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        #region table
        builder.ToTable("user_permissions");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(up => up.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(up => up.PermissionId)
            .HasColumnName("permission_id")
            .IsRequired();

        builder.Property(up => up.AuthorId)
            .HasColumnName("author_id")
            .IsRequired(false);

        builder.Property(up => up.CreateAt)
            .HasColumnName("created_at")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(up => up.User)
            .WithMany(u => u.UserPermissions)
            .HasForeignKey(up => up.UserId)
            .IsRequired();

        builder.HasOne(up => up.UserAuthor)
            .WithMany(u => u.UserPermissionsAuthor)
            .HasForeignKey(up => up.AuthorId)
            .IsRequired(false);

        builder.HasOne(up => up.Permission)
            .WithMany(p => p.UserPermissions)
            .HasForeignKey(up => up.PermissionId)
            .IsRequired();
        #endregion

        #region ignore
        #endregion

        #region index
        // Поиск разрешений конкретного пользователя
        builder.HasIndex(up => up.UserId)
            .HasDatabaseName("ix_user_permissions_user_id");

        // Поиск по разрешению
        builder.HasIndex(up => up.PermissionId)
            .HasDatabaseName("ix_user_permissions_permission_id");

        // Поиск по автору (кто выдал разрешение)
        builder.HasIndex(up => up.AuthorId)
            .HasDatabaseName("ix_user_permissions_author_id");

        // Комбинированный индекс для поиска разрешений пользователя
        builder.HasIndex(up => new { up.UserId, up.PermissionId })
            .HasDatabaseName("ix_user_permissions_user_permission");
        #endregion
    }
}