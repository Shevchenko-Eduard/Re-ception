using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Guest;

public class GuestConfig : IEntityTypeConfiguration<Domain.Entity.Guest.Guest>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Guest.Guest> builder)
    {
        // Configuration here
    }
}
