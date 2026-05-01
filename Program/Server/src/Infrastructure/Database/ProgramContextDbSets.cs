using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public partial class ProgramContext
{
    public DbSet<Domain.Entity.Hotel.Hotel> Hotels { get; set; }
    public DbSet<Domain.Entity.Hotel.HotelTag> HotelTags { get; set; }
    public DbSet<Domain.Entity.Hotel.HotelHotelTag> HotelHotelTags { get; set; }
    public DbSet<Domain.Entity.Hotel.HotelImage> HotelImages { get; set; }

    public DbSet<Domain.Entity.Payment.Payment> Payments { get; set; }
    public DbSet<Domain.Entity.Payment.PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Domain.Entity.Payment.PaymentStatus> PaymentStatuses { get; set; }

    public DbSet<Domain.Entity.Reservation.Reservation> Reservations { get; set; }
    public DbSet<Domain.Entity.Reservation.ReservationStatus> ReservationStatuses { get; set; }

    public DbSet<Domain.Entity.Room.Room> Rooms { get; set; }
    public DbSet<Domain.Entity.Room.RoomStatus> RoomStatuses { get; set; }
    public DbSet<Domain.Entity.Room.RoomTag> RoomTags { get; set; }
    public DbSet<Domain.Entity.Room.RoomRoomTag> RoomRoomTags { get; set; }
    public DbSet<Domain.Entity.Room.RoomImage> RoomImages { get; set; }
    public DbSet<Domain.Entity.Room.RoomType> RoomTypes { get; set; }
}