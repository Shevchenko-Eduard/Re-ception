using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public partial class ProgramContext
{
    public DbSet<Domain.Entity.Employee.Employee> Employees { get; set; }

    public DbSet<Domain.Entity.Guest.Guest> Guests { get; set; }

    public DbSet<Domain.Entity.Hotel.Hotel> Hotels { get; set; }
    public DbSet<Domain.Entity.Hotel.HotelTag> HotelTags { get; set; }

    public DbSet<Domain.Entity.Payment.Payment> Payments { get; set; }
    public DbSet<Domain.Entity.Payment.PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Domain.Entity.Payment.PaymentStatus> PaymentStatuses { get; set; }

    public DbSet<Domain.Entity.Reservation.Reservation> Reservations { get; set; }
    public DbSet<Domain.Entity.Reservation.ReservationStatus> ReservationStatuses { get; set; }

    public DbSet<Domain.Entity.Room.Room> Rooms { get; set; }
    public DbSet<Domain.Entity.Room.RoomStatus> RoomStatuses { get; set; }
    public DbSet<Domain.Entity.Room.RoomTag> RoomTags { get; set; }
    public DbSet<Domain.Entity.Room.RoomType> RoomTypes { get; set; }

    public DbSet<Domain.Entity.User.Permission.Permission> Permissions { get; set; }
    public DbSet<Domain.Entity.User.Permission.PermissionAction> PermissionActions { get; set; }
    public DbSet<Domain.Entity.User.Permission.PermissionEntity> PermissionEntities { get; set; }
    public DbSet<Domain.Entity.User.Permission.PermissionFlag> PermissionFlags { get; set; }

    public DbSet<Domain.Entity.User.Role.Role> Roles { get; set; }

    public DbSet<Domain.Entity.User.User> AppUsers { get; set; }
    public DbSet<Domain.Entity.User.UserGender> UserGenders { get; set; }
    public DbSet<Domain.Entity.User.UserPermission> UserPermissions { get; set; }
    public DbSet<Domain.Entity.User.UserRole> UserRoles { get; set; }
}