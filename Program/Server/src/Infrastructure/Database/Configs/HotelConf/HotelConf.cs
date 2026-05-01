using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.HotelConf;

public class HotelConf : IEntityTypeConfiguration<Domain.Entity.Hotel.Hotel>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Hotel.Hotel> builder)
    {
        #region table
        builder.ToTable("hotels");
        #endregion

        #region pk
        builder.HasKey(h => h.Id)
            .HasName("hotel_id");
        #endregion

        #region property
        builder.Property(h => h.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(h => h.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);

        builder.Property(h => h.Email)
            .HasColumnName("email")
            .HasMaxLength(254)
            .IsRequired();

        builder.Property(h => h.Phone)
            .HasColumnName("phone")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(h => h.Latitude)
            .HasColumnName("latitude")
            .HasPrecision(9, 6)
            .IsRequired();

        builder.Property(h => h.Longitude)
            .HasColumnName("longitude")
            .HasPrecision(9, 6)
            .IsRequired();
        #endregion

        #region fk
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(h => new { h.Latitude, h.Longitude })
            .HasDatabaseName("idx_hotels_lati_long");

        builder.HasIndex(h => h.Email)
            .IsUnique()
            .HasDatabaseName("uq_hotels_email");

        builder.HasIndex(h => h.Phone)
            .IsUnique()
            .HasDatabaseName("uq_hotels_phone");
        #endregion
    }
}
