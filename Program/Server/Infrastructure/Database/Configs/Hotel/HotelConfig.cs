using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Hotel;

public class HotelConfig : IEntityTypeConfiguration<Domain.Entity.Hotel.Hotel>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Hotel.Hotel> builder)
    {
        #region table
        builder.ToTable("hotels");
        #endregion

        #region pk
        builder.HasKey(h => h.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(h => h.Country)
            .HasColumnName("country")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(h => h.Region)
            .HasColumnName("region")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(h => h.City)
            .HasColumnName("city")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(h => h.Street)
            .HasColumnName("street")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(h => h.Address)
            .HasColumnName("address")
            .HasMaxLength(100)
            .IsRequired();

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
        builder.HasMany(h => h.Employees)
            .WithOne(e => e.Hotel)
            .HasForeignKey(e => e.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(h => h.HotelTags)
            .WithMany(t => t.Hotels);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(h => new { h.Latitude, h.Longitude })
            .HasDatabaseName("ix_hotels_coordinates");

        builder.HasIndex(h => h.Email)
            .IsUnique()
            .HasDatabaseName("ux_hotels_email");

        builder.HasIndex(h => h.Phone)
            .IsUnique()
            .HasDatabaseName("ux_hotels_phone");
        #endregion
    }
}
