using Domain.Entity.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.HotelConf;

public class HotelTagConf : IEntityTypeConfiguration<HotelTag>
{
    public void Configure(EntityTypeBuilder<HotelTag> builder)
    {
        #region table
        builder.ToTable("hotel_tags");
        #endregion

        #region pk
        builder.HasKey(t => t.Id)
            .HasName("hotel_tag_id");
        #endregion

        #region property
        builder.Property(t => t.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasColumnName("description")
            .HasMaxLength(500);
        #endregion

        #region fk
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(t => t.Name)
            .IsUnique()
            .HasDatabaseName("uq_hotel_tags_tag_name");
        #endregion
    }
}
