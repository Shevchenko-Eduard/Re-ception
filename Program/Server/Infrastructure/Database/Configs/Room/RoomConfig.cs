using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Room;

public class RoomConfig : IEntityTypeConfiguration<Domain.Entity.Room.Room>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Room.Room> builder)
    {
        #region table
        builder.ToTable("rooms");
        #endregion

        #region pk
        builder.HasKey(r => r.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(r => r.HotelId)
            .HasColumnName("hotel_id")
            .IsRequired();

        builder.Property(r => r.RoomTypeId)
            .HasColumnName("room_type_id")
            .IsRequired();

        builder.Property(r => r.RoomNumber)
            .HasColumnName("room_number")
            .IsRequired();

        builder.Property(r => r.Floor)
            .HasColumnName("floor")
            .IsRequired();

        builder.Property(r => r.RoomStatusId)
            .HasColumnName("room_status_id")
            .IsRequired();

        builder.Property(r => r.PricePerDay)
            .HasColumnName("price_per_day")
            .HasPrecision(19, 4);
        #endregion

        #region fk
        builder.HasOne(r => r.Hotel)
            .WithMany(h => h.Rooms)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.RoomType)
            .WithMany(rt => rt.Rooms)
            .HasForeignKey(r => r.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.RoomStatus)
            .WithMany(rs => rs.Rooms)
            .HasForeignKey(r => r.RoomStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.RoomTags)
            .WithMany(rt => rt.Rooms);

        builder.HasMany(r => r.Reservations)
            .WithOne(res => res.Room)
            .HasForeignKey(res => res.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(r => new { r.HotelId, r.RoomNumber })
            .IsUnique()
            .HasDatabaseName("ux_rooms_hotel_room_number");

        builder.HasIndex(r => r.RoomStatusId)
            .HasDatabaseName("ix_rooms_status_id");

        builder.HasIndex(r => new { r.HotelId, r.Floor })
            .HasDatabaseName("ix_rooms_hotel_floor");
        #endregion
    }
}
