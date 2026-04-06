using Domain.Entity.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Room;

public class RoomStatusConfig : IEntityTypeConfiguration<RoomStatus>
{
    public void Configure(EntityTypeBuilder<RoomStatus> builder)
    {
        // Configuration here
    }
}
