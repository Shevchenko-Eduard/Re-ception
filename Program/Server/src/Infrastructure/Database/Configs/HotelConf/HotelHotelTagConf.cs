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

        builder.Property(hht => hht.Tag)
            .HasColumnName("Hotel_tag_id")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(hht => hht.Hotel)
            .WithMany(h => h.HotelHotelTags)
            .HasForeignKey(hht => hht.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(hht => hht.Tag)
            .WithMany(t => t.HotelTags)
            .HasForeignKey(hht => hht.TagId)
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(hht => new{ hht.HotelId, hht.TagId})
            .IsUnique()
            .HasDatabaseName("uq_hotel_hotel_tags_hid_htid");
        #endregion
    }
}