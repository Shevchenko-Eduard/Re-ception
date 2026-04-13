using Domain.Entity.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Reservation;

public class ReservationStatusConfig : IEntityTypeConfiguration<ReservationStatus>
{
    public void Configure(EntityTypeBuilder<ReservationStatus> builder)
    {
        #region table
        builder.ToTable("reservation_statuses");
        #endregion

        #region pk
        builder.HasKey(rs => rs.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(rs => rs.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        #endregion

        #region fk
        builder.HasMany(rs => rs.Reservations)
            .WithOne(r => r.ReservationStatus)
            .HasForeignKey(r => r.ReservationStatusId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region seed_data
        builder.HasData(ReservationStatus.All);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(rs => rs.Name)
            .IsUnique()
            .HasDatabaseName("ux_reservation_statuses_name");
        #endregion
    }
}
