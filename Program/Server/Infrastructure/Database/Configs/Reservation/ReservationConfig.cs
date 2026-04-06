using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Reservation;

public class ReservationConfig : IEntityTypeConfiguration<Domain.Entity.Reservation.Reservation>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Reservation.Reservation> builder)
    {
        // Configuration here
    }
}
