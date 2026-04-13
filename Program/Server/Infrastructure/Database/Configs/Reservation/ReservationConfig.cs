using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Reservation;

public class ReservationConfig : IEntityTypeConfiguration<Domain.Entity.Reservation.Reservation>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Reservation.Reservation> builder)
    {
        #region table
        builder.ToTable("reservations");
        #endregion

        #region pk
        builder.HasKey(r => r.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(r => r.GuestId)
            .HasColumnName("guest_id")
            .IsRequired();

        builder.Property(r => r.RoomId)
            .HasColumnName("room_id")
            .IsRequired();

        builder.Property(r => r.CheckIn)
            .HasColumnName("check_in")
            .IsRequired();

        builder.Property(r => r.CheckOut)
            .HasColumnName("check_out")
            .IsRequired();

        builder.Property(r => r.ReservationStatusId)
            .HasColumnName("reservation_status_id")
            .IsRequired();

        builder.Property(r => r.CreateAt)
            .HasColumnName("create_at")
            .IsRequired();

        builder.Property(r => r.TotalPrice)
            .HasColumnName("total_price")
            .HasPrecision(19, 4)
            .IsRequired();

        builder.Property(r => r.Discount)
            .HasColumnName("discount")
            .HasPrecision(19, 4);
        #endregion

        #region fk
        builder.HasOne(r => r.Guest)
            .WithMany(g => g.Reservations)
            .HasForeignKey(r => r.GuestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Room)
            .WithMany(rm => rm.Reservations)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ReservationStatus)
            .WithMany(rs => rs.Reservations)
            .HasForeignKey(r => r.ReservationStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Payments)
            .WithOne(p => p.Reservation)
            .HasForeignKey(p => p.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(r => r.GuestId)
            .HasDatabaseName("ix_reservations_guest_id");

        builder.HasIndex(r => r.RoomId)
            .HasDatabaseName("ix_reservations_room_id");

        builder.HasIndex(r => r.ReservationStatusId)
            .HasDatabaseName("ix_reservations_status_id");

        builder.HasIndex(r => new { r.CheckIn, r.CheckOut })
            .HasDatabaseName("ix_reservations_dates");
        #endregion
    }
}
