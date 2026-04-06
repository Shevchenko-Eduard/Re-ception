using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class PermissionEntity : StatusWithParentsObjectsAbstract<PermissionEntity>
{
    #region Constructors
    private PermissionEntity(string name) : base(name) { }
    private PermissionEntity(string name, params IEnumerable<PermissionEntity> parents) : base(name, parents) { }
    #endregion
    #region Default objects
    public static readonly PermissionEntity User = new(nameof(User));
    public static readonly PermissionEntity Role = new(nameof(Role));
    public static readonly PermissionEntity Permission = new(nameof(Permission));
    public static readonly PermissionEntity Payment = new(nameof(Payment));
    public static readonly PermissionEntity Reservation = new(nameof(Reservation));
    public static readonly PermissionEntity Guest = new(nameof(Guest));
    public static readonly PermissionEntity HotelTag = new(nameof(HotelTag));
    public static readonly PermissionEntity Hotel = new(nameof(Hotel));
    public static readonly PermissionEntity Room = new(nameof(Room));
    public static readonly PermissionEntity RoomTag = new(nameof(RoomTag));
    public static readonly PermissionEntity RoomType = new(nameof(RoomType));
    public static readonly PermissionEntity Employee = new(nameof(Employee));
    public static readonly PermissionEntity EmployeeRole = new(nameof(EmployeeRole));
    public static readonly PermissionEntity EmployeePermission = new(nameof(EmployeePermission));
    public static readonly PermissionEntity Super = new(nameof(Super), All);
    #endregion
    #region Navigation properties
    public ICollection<Permission>? Permissions { get; private set; }
    #endregion
}