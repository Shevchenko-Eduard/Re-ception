using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class Permission : EnumObjectAbstract<Permission>
{
    #region Fields
    public byte ActionId { get; init; }
    public byte EntityId { get; init; }
    public byte FlagId { get; init; }
    #endregion
    #region Navigation properties
    public ICollection<UserRole>? UserRoles { get; private set; }
    public ICollection<UserPermission>? UserPermissions { get; private set; }
    public PermissionAction? PermissionAction { get; private set; }
    public PermissionEntity? PermissionEntity { get; private set; }
    public PermissionFlag? PermissionFlag { get; private set; }
    #endregion
    #region Constructors
    private Permission(byte id, byte actionId, byte entityId, byte flagId) : base(id)
    {
        ActionId = actionId;
        EntityId = entityId;
        FlagId = flagId;
    }
    #endregion
    #region Default objects
    public readonly static Permission Super = new(0, 0, 0, 0);
    #endregion
    #region Methods
    public override bool Equals(object? obj)
    {
        if (obj is Permission permission) { return Equals(permission: permission); }
        throw new TypeAccessException();
    }
    /// <summary>
    /// Текущий Permission проверяется на соответствие входящему аргументу Permission.
    /// </summary>
    public bool Equals(Permission permission)
    {
        if (permission.IsNavigationProperties() && IsNavigationProperties())
        {
            return PermissionAction!.Equals(permission.PermissionAction!) &&
                PermissionEntity!.Equals(permission.PermissionEntity!) &&
                (PermissionFlag?.Equals(permission.PermissionFlag!) == true);
        }
        throw new ArgumentException(message: "First, you need to load the navigation properties.");
    }
    public override int GetHashCode() => Id;
    private bool IsNavigationProperties() => PermissionAction is not null && PermissionEntity is not null;
    #endregion
}