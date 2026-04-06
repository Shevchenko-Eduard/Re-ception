using Domain.Entity.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Hotel;

public class HotelTagConfig : IEntityTypeConfiguration<HotelTag>
{
    public void Configure(EntityTypeBuilder<HotelTag> builder)
    {
        // Configuration here
    }
}
