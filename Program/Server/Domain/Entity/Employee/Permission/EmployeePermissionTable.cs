using Domain.Abstract;

namespace Domain.Entity.Employee.Permission;

public sealed class EmployeePermissionTable : StatusObjectAbstract<EmployeePermissionTable>
{
    #region Constructors
    private EmployeePermissionTable(byte id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly EmployeePermissionTable Payment = new(0, nameof(Payment));
    public static readonly EmployeePermissionTable Reservation = new(1, nameof(Reservation));
    public static readonly EmployeePermissionTable Guest = new(2, nameof(Guest));
    public static readonly EmployeePermissionTable HotelTag = new(3, nameof(HotelTag));
    public static readonly EmployeePermissionTable Hotel = new(4, nameof(Hotel));
    public static readonly EmployeePermissionTable Room = new(5, nameof(Room));
    public static readonly EmployeePermissionTable RoomTag = new(6, nameof(RoomTag));
    public static readonly EmployeePermissionTable RoomType = new(7, nameof(RoomType));
    public static readonly EmployeePermissionTable Employee = new(8, nameof(Employee));
    public static readonly EmployeePermissionTable EmployeeRole = new(9, nameof(EmployeeRole));
    public static readonly EmployeePermissionTable EmployeePermission = new(10, nameof(EmployeePermission));
    #endregion
}