using Domain.Entity.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.RoomConf;

public class RoomStatusConf : IEntityTypeConfiguration<RoomStatus>
{
    public void Configure(EntityTypeBuilder<RoomStatus> builder)
    {
        #region table
        builder.ToTable("room_statuses");
        #endregion

        #region pk
        builder.HasKey(rs => rs.Id)
            .HasName("room_status_id");
        #endregion

        #region property
        builder.Property(rs => rs.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        #endregion

        #region fk
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(rs => rs.Name)
            .IsUnique()
            .HasDatabaseName("uq_room_statuses_name");
        #endregion
    }
}
