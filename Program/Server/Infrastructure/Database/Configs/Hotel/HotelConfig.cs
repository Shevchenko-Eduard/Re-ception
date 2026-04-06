using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Hotel;

public class HotelConfig : IEntityTypeConfiguration<Domain.Entity.Hotel.Hotel>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Hotel.Hotel> builder)
    {
        // Configuration here
    }
}
