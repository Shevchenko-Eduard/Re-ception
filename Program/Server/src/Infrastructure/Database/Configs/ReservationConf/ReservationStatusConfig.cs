using Domain.Entity.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.ReservationConf;

public class ReservationStatusConfig : IEntityTypeConfiguration<ReservationStatus>
{
    public void Configure(EntityTypeBuilder<ReservationStatus> builder)
    {
        #region table
        builder.ToTable("reservation_statuses");
        #endregion

        #region pk
        builder.HasKey(rs => rs.Id)
            .HasName("reservation_status_id");
        #endregion

        #region property
        builder.Property(rs => rs.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        #endregion

        #region fk
        #endregion

        #region seed_data
        builder.HasData(ReservationStatus.All);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(rs => rs.Name)
            .IsUnique()
            .HasDatabaseName("uq_reservation_statuses_name");
        #endregion
    }
}
