using Infrastructure.Database.IdentityEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.IdentityEntity;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        #region table
        builder.ToTable("identity_users");
        #endregion

        #region pk
        // Id наследуется от IdentityUser
        #endregion

        #region property
        builder.Property(au => au.AppUserId)
            .HasColumnName("app_user_id")
            .IsRequired();

        builder.Property(u => u.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(u => u.NormalizedUserName)
            .HasColumnName("normalized_user_name")
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(u => u.NormalizedEmail)
            .HasColumnName("normalized_email")
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(20)
            .IsRequired(false);
        #endregion

        #region fk
        // Связь между ApplicationUser и User
        builder.HasOne<Domain.Entity.User.User>()
            .WithMany()
            .HasForeignKey(au => au.AppUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        #endregion

        #region ignore
        #endregion

        #region index
        // Поиск по AppUserId
        builder.HasIndex(au => au.AppUserId)
            .HasDatabaseName("ix_identity_users_app_user_id")
            .IsUnique();

        // Поиск по email для авторизации
        builder.HasIndex(u => u.NormalizedEmail)
            .HasDatabaseName("ix_identity_users_normalized_email");

        // Поиск по username для авторизации
        builder.HasIndex(u => u.NormalizedUserName)
            .HasDatabaseName("ix_identity_users_normalized_user_name");
        #endregion
    }
}
