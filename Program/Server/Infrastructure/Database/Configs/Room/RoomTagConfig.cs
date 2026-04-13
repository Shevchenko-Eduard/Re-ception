using Domain.Entity.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Room;

public class RoomTagConfig : IEntityTypeConfiguration<RoomTag>
{
    public void Configure(EntityTypeBuilder<RoomTag> builder)
    {
        #region table
        builder.ToTable("room_tags");
        #endregion

        #region pk
        builder.HasKey(rt => rt.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(rt => rt.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(rt => rt.Description)
            .HasColumnName("description")
            .HasMaxLength(250);
        #endregion

        #region fk
        builder.HasMany(rt => rt.Rooms)
            .WithMany(r => r.RoomTags);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(rt => rt.Name)
            .IsUnique()
            .HasDatabaseName("ux_room_tags_name");
        #endregion
    }
}
