using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class PermissionFlag: StatusObjectAbstract<PermissionFlag>
{
    #region Constructors
    private PermissionFlag(byte id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly PermissionFlag Create = new(0, nameof(Create));
    public static readonly PermissionFlag Read = new(1, nameof(Read));
    public static readonly PermissionFlag Update = new(2, nameof(Update));
    public static readonly PermissionFlag Delete = new(3, nameof(Delete));
    #endregion
}