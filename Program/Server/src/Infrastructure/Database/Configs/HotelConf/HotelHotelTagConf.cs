using Domain.Entity.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.HotelConf;

public class HotelHotelTagConf : IEntityTypeConfiguration<HotelHotelTag>
{
    public void Configure(EntityTypeBuilder<HotelHotelTag> builder)
    {
        #region table
        builder.ToTable("hotel_hotel_tags");
        #endregion

        #region pk
        builder.HasKey(hht => hht.Id)
            .HasName("hotel_hotel_tag_id");
        #endregion

        #region property
        builder.Property(hht => hht.HotelId)
            .HasColumnName("hotel_id")
            .IsRequired();

        builder.Property(hht => hht.HotelTagId)
            .HasColumnName("Hotel_tag_id")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(hht => hht.Hotel)
            .WithMany(h => h.HotelHotelTags)
            .HasForeignKey(hht => hht.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(hht => hht.HotelTag)
            .WithMany(t => t.HotelHotelTags)
            .HasForeignKey(hht => hht.HotelTagId)
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(hht => new{ hht.HotelId, hht.HotelTagId})
            .IsUnique()
            .HasDatabaseName("uq_hotel_hotel_tags_hid_htid");
        #endregion
    }
}