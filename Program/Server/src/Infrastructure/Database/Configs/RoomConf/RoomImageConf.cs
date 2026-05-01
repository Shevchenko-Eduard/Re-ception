using Domain.Entity.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.RoomConf;

public class RoomImageConf : IEntityTypeConfiguration<RoomImage>
{
    public void Configure(EntityTypeBuilder<RoomImage> builder)
    {
        #region table
        builder.ToTable("room_images");
        #endregion

        #region pk
        builder.HasKey(i => i.Id)
            .HasName("room_image_id");
        #endregion

        #region property
        builder.Property(i => i.RoomId)
            .HasColumnName("room_id");

        builder.Property(i => i.ImageKey)
            .HasColumnName("image_key")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(i => i.Room)
            .WithMany(r => r.RoomImages)
            .HasForeignKey(i => i.RoomId)
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(i => i.ImageKey)
            .IsUnique()
            .HasDatabaseName("uq_room_images_image_key");
        #endregion
    }
}