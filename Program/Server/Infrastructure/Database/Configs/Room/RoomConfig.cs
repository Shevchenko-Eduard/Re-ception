using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Room;

public class RoomConfig : IEntityTypeConfiguration<Domain.Entity.Room.Room>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Room.Room> builder)
    {
        // Configuration here
    }
}
