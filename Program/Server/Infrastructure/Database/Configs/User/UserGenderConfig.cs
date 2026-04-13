using Domain.Entity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User;

public class UserGenderConfig : IEntityTypeConfiguration<UserGender>
{
    public void Configure(EntityTypeBuilder<UserGender> builder)
    {
        #region table
        builder.ToTable("user_genders");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(ug => ug.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        #endregion

        #region fk
        builder.HasMany(ug => ug.Users)
            .WithOne(u => u.UserGender)
            .HasForeignKey(u => u.GenderId);
        #endregion

        #region ignore
        #endregion

        #region index
        // Поиск по названию пола
        builder.HasIndex(ug => ug.Name)
            .HasDatabaseName("ix_user_genders_name")
            .IsUnique();
        #endregion
    }
}