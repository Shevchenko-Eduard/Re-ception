using Domain.Entity.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.RoomConf;

public class RoomTagConf : IEntityTypeConfiguration<RoomTag>
{
    public void Configure(EntityTypeBuilder<RoomTag> builder)
    {
        #region table
        builder.ToTable("room_tags");
        #endregion

        #region pk
        builder.HasKey(rt => rt.Id)
            .HasName("room_tag_id");
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
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(rt => rt.Name)
            .IsUnique()
            .HasDatabaseName("uq_room_tags_name");
        #endregion
    }
}
