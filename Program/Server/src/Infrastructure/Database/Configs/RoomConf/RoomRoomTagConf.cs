using Domain.Entity.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.RoomConf;

public class RoomRoomTagConf : IEntityTypeConfiguration<RoomRoomTag>
{
    public void Configure(EntityTypeBuilder<RoomRoomTag> builder)
    {
        #region table
        builder.ToTable("room_room_tags");
        #endregion

        #region pk
        builder.HasKey(rrt => rrt.Id)
            .HasName("room_room_tag_id");
        #endregion

        #region property
        builder.Property(rrt => rrt.RoomId)
            .HasColumnName("room_id")
            .IsRequired();

        builder.Property(rrt => rrt.RoomTagId)
            .HasColumnName("room_tag_id")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(rrt => rrt.Room)
            .WithMany(h => h.RoomRoomTags)
            .HasForeignKey(rrt => rrt.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rrt => rrt.RoomTag)
            .WithMany(t => t.RoomRoomTags)
            .HasForeignKey(rrt => rrt.RoomTagId)
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(rrt => new{ rrt.RoomId, rrt.RoomTagId})
            .IsUnique()
            .HasDatabaseName("uq_room_room_tags_rid_rtid");
        #endregion
    }
}