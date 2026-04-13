using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User;

public class UserConfig : IEntityTypeConfiguration<Domain.Entity.User.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.User.User> builder)
    {
        #region table
        builder.ToTable("users");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(u => u.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.DateOfBirth)
            .HasColumnName("date_of_birth")
            .IsRequired();

        builder.Property(u => u.CreateAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(u => u.GenderId)
            .HasColumnName("gender_id")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(u => u.UserGender)
            .WithMany(g => g.Users)
            .HasForeignKey(u => u.GenderId)
            .IsRequired();

        builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId);

        builder.HasMany(u => u.UserPermissions)
            .WithOne(up => up.User)
            .HasForeignKey(up => up.UserId);

        builder.HasMany(u => u.UserRolesAuthor)
            .WithOne(ur => ur.UserAuthor)
            .HasForeignKey(ur => ur.AuthorId);

        builder.HasMany(u => u.UserPermissionsAuthor)
            .WithOne(up => up.UserAuthor)
            .HasForeignKey(up => up.AuthorId);
        #endregion

        #region ignore
        builder.Ignore(u => u.UserGender);
        #endregion

        #region index
        // Поиск пользователей по имени
        builder.HasIndex(u => u.UserName)
            .HasDatabaseName("ix_users_user_name");

        // Фильтр по полу
        builder.HasIndex(u => u.GenderId)
            .HasDatabaseName("ix_users_gender_id");

        // Фильтр по дате создания
        builder.HasIndex(u => u.CreateAt)
            .HasDatabaseName("ix_users_created_at");
        #endregion
    }
}