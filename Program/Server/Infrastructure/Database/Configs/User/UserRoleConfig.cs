using Domain.Entity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User;

public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        #region table
        builder.ToTable("user_roles");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(ur => ur.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(ur => ur.RoleId)
            .HasColumnName("role_id")
            .IsRequired();

        builder.Property(ur => ur.AuthorId)
            .HasColumnName("author_id")
            .IsRequired(false);

        builder.Property(ur => ur.CreateAt)
            .HasColumnName("created_at")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.HasOne(ur => ur.UserAuthor)
            .WithMany(u => u.UserRolesAuthor)
            .HasForeignKey(ur => ur.AuthorId)
            .IsRequired(false);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
        #endregion

        #region ignore
        #endregion

        #region index
        // Поиск ролей конкретного пользователя
        builder.HasIndex(ur => ur.UserId)
            .HasDatabaseName("ix_user_roles_user_id");

        // Поиск по роли
        builder.HasIndex(ur => ur.RoleId)
            .HasDatabaseName("ix_user_roles_role_id");

        // Поиск по автору (кто выдал роль)
        builder.HasIndex(ur => ur.AuthorId)
            .HasDatabaseName("ix_user_roles_author_id");

        // Комбинированный индекс для поиска ролей пользователя
        builder.HasIndex(ur => new { ur.UserId, ur.RoleId })
            .HasDatabaseName("ix_user_roles_user_role");
        #endregion
    }
}