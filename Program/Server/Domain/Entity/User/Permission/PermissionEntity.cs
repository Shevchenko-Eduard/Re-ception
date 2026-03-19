using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class PermissionEntity : StatusWithParentsObjectsAbstract<PermissionEntity>
{
    #region Constructors
    private PermissionEntity(byte id, string name) : base(id, name) { }
    private PermissionEntity(byte id, string name, params IEnumerable<PermissionEntity> parents) : base(id, name, parents) { }
    #endregion
    #region Default objects
    public static readonly PermissionEntity Super = new(0, nameof(Super), All);
    public static readonly PermissionEntity Payment = new(1, nameof(Payment));
    public static readonly PermissionEntity Reservation = new(2, nameof(Reservation));
    public static readonly PermissionEntity Guest = new(3, nameof(Guest));
    public static readonly PermissionEntity HotelTag = new(4, nameof(HotelTag));
    public static readonly PermissionEntity Hotel = new(5, nameof(Hotel));
    public static readonly PermissionEntity Room = new(6, nameof(Room));
    public static readonly PermissionEntity RoomTag = new(7, nameof(RoomTag));
    public static readonly PermissionEntity RoomType = new(8, nameof(RoomType));
    public static readonly PermissionEntity Employee = new(9, nameof(Employee));
    public static readonly PermissionEntity EmployeeRole = new(10, nameof(EmployeeRole));
    public static readonly PermissionEntity EmployeePermission = new(11, nameof(EmployeePermission));
    #endregion
}