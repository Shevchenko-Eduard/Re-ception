using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Guest;

public class GuestConfig : IEntityTypeConfiguration<Domain.Entity.Guest.Guest>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Guest.Guest> builder)
    {
        #region table
        builder.ToTable("guests");
        #endregion

        #region pk
        builder.HasKey(g => g.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(g => g.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        builder.Property(g => g.CreateAt)
            .HasColumnName("create_at")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(g => g.User)
            .WithOne(u => u.Guest)
            .HasForeignKey<Domain.Entity.Guest.Guest>(g => g.UserId)
            .IsRequired();
        #endregion

        #region ignore
        #endregion

        #region index
        #endregion
    }
}
