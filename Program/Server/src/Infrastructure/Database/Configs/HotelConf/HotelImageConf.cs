using Domain.Entity.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.HotelConf;

public class HotelImageConf : IEntityTypeConfiguration<HotelImage>
{
    public void Configure(EntityTypeBuilder<HotelImage> builder)
    {
        #region table
        builder.ToTable("hotel_images");
        #endregion

        #region pk
        builder.HasKey(i => i.Id)
            .HasName("hotel_image_id");
        #endregion

        #region property
        builder.Property(i => i.HotelId)
            .HasColumnName("hotel_id");

        builder.Property(i => i.ImageKey)
            .HasColumnName("image_key")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(i => i.Hotel)
            .WithMany(h => h.HotelImages)
            .HasForeignKey(i => i.HotelId)
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(i => i.ImageKey)
            .IsUnique()
            .HasDatabaseName("uq_hotel_images_image_key");
        #endregion
    }
}