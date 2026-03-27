using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class PermissionFlag: StatusWithParentsObjectsAbstract<PermissionFlag>
{
    #region Constructors
    private PermissionFlag(byte id, string name) : base(id, name) { }
    private PermissionFlag(byte id, string name, params IEnumerable<PermissionFlag> parents) : base(id, name) { Parents = parents; }
    #endregion
    #region Default objects
    public static readonly PermissionFlag Super = new(0, nameof(Super), All);
    public static readonly PermissionFlag Self = new(1, nameof(Self));
    public static readonly PermissionFlag Any = new(2, nameof(Any), Self);
    #endregion
    #region Navigation properties
    public ICollection<Permission>? Permissions { get; private set; }
    #endregion
}