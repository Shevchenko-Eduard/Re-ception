using Domain.Entity.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.RoomConf;

public class RoomTypeConf : IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        #region table
        builder.ToTable("room_types");
        #endregion

        #region pk
        builder.HasKey(rt => rt.Id)
            .HasName("room_type_id");
        #endregion

        #region property
        builder.Property(rt => rt.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(rt => rt.Description)
            .HasColumnName("description")
            .HasMaxLength(250);

        builder.Property(rt => rt.BasePricePerDay)
            .HasColumnName("base_price_per_day")
            .HasPrecision(19, 4)
            .IsRequired();

        builder.Property(rt => rt.MaxCountGuest)
            .HasColumnName("max_count_guest")
            .IsRequired();
        #endregion

        #region fk
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(rt => rt.Name)
            .IsUnique()
            .HasDatabaseName("uq_room_types_name");
        #endregion
    }
}
