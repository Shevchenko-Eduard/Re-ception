using Domain.Entity.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Hotel;

public class HotelTagConfig : IEntityTypeConfiguration<HotelTag>
{
    public void Configure(EntityTypeBuilder<HotelTag> builder)
    {
        #region table
        builder.ToTable("hotel_tags");
        #endregion

        #region pk
        builder.HasKey(t => t.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(t => t.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasColumnName("description");
        #endregion

        #region fk
        builder.HasMany(h => h.Hotels)
            .WithMany(t => t.HotelTags);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(t => t.Name)
            .IsUnique()
            .HasDatabaseName("ux_hotel_tags_name");
        #endregion
    }
}
