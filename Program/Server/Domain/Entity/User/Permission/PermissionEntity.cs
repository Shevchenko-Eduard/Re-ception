using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class PermissionEntity : StatusObjectAbstract<PermissionEntity>
{
    #region Constructors
    private PermissionEntity(byte id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly PermissionEntity Payment = new(0, nameof(Payment));
    public static readonly PermissionEntity Reservation = new(1, nameof(Reservation));
    public static readonly PermissionEntity Guest = new(2, nameof(Guest));
    public static readonly PermissionEntity HotelTag = new(3, nameof(HotelTag));
    public static readonly PermissionEntity Hotel = new(4, nameof(Hotel));
    public static readonly PermissionEntity Room = new(5, nameof(Room));
    public static readonly PermissionEntity RoomTag = new(6, nameof(RoomTag));
    public static readonly PermissionEntity RoomType = new(7, nameof(RoomType));
    public static readonly PermissionEntity Employee = new(8, nameof(Employee));
    public static readonly PermissionEntity EmployeeRole = new(9, nameof(EmployeeRole));
    public static readonly PermissionEntity EmployeePermission = new(10, nameof(EmployeePermission));
    #endregion
}