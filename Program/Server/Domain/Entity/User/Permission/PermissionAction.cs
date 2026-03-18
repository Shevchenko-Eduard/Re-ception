using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class PermissionAction: StatusObjectAbstract<PermissionAction>
{
    #region Constructors
    private PermissionAction(byte id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly PermissionAction Create = new(0, nameof(Create));
    public static readonly PermissionAction Read = new(1, nameof(Read));
    public static readonly PermissionAction Update = new(2, nameof(Update));
    public static readonly PermissionAction Delete = new(3, nameof(Delete));
    #endregion
}