using Domain.Abstract;

namespace Domain.Entity.Employee.Permission;

public sealed class EmployeePermissionAction: StatusObjectAbstract<EmployeePermissionAction>
{
    #region Constructors
    private EmployeePermissionAction(byte id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly EmployeePermissionAction Create = new(0, nameof(Create));
    public static readonly EmployeePermissionAction Read = new(1, nameof(Read));
    public static readonly EmployeePermissionAction Update = new(2, nameof(Update));
    public static readonly EmployeePermissionAction Delete = new(3, nameof(Delete));
    #endregion
}