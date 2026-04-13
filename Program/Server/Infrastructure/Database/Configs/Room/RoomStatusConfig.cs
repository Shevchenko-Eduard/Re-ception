using Domain.Entity.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Room;

public class RoomStatusConfig : IEntityTypeConfiguration<RoomStatus>
{
    public void Configure(EntityTypeBuilder<RoomStatus> builder)
    {
        #region table
        builder.ToTable("room_statuses");
        #endregion

        #region pk
        builder.HasKey(rs => rs.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(rs => rs.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        #endregion

        #region fk
        builder.HasMany(rs => rs.Rooms)
            .WithOne(r => r.RoomStatus)
            .HasForeignKey(r => r.RoomStatusId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(rs => rs.Name)
            .IsUnique()
            .HasDatabaseName("ux_room_statuses_name");
        #endregion
    }
}
