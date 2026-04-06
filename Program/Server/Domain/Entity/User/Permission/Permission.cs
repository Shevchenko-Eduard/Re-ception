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
    public ICollection<Role.Role>? Roles { get; private set; }
    public ICollection<UserPermission>? UserPermissions { get; private set; }
    public PermissionAction? PermissionAction { get; private set; }
    public PermissionEntity? PermissionEntity { get; private set; }
    public PermissionFlag? PermissionFlag { get; private set; }
    #endregion
    #region Constructors
    public Permission(byte actionId, byte entityId, byte flagId) : base()
    {
        ActionId = actionId;
        EntityId = entityId;
        FlagId = flagId;
    }
    public Permission(
        PermissionAction permissionAction,
        PermissionEntity permissionEntity,
        PermissionFlag permissionFlag) : this(
            actionId: permissionAction.Id,
            entityId: permissionEntity.Id,
            flagId: permissionFlag.Id)
    {
        PermissionAction = permissionAction;
        PermissionEntity = permissionEntity;
        PermissionFlag = permissionFlag;
    }
    #endregion
    #region Default objects
    public readonly static Permission Super = new(PermissionAction.Super, PermissionEntity.Super, PermissionFlag.Super);
    #endregion
    #region Methods
    public override bool Equals(object? obj)
    {
        if (obj is Permission permission) { return Equals(statementPermission: permission); }
        throw new TypeAccessException();
    }
    /// <summary>
    /// Текущий Permission проверяется на соответствие входящему аргументу Permission.
    /// </summary>
    public bool Equals(Permission statementPermission)
    {
        if (statementPermission.IsNavigationProperties() && IsNavigationProperties())
        {
            return PermissionAction!.Equals(statementPermission.PermissionAction!) &&
                PermissionEntity!.Equals(statementPermission.PermissionEntity!) &&
                (PermissionFlag?.Equals(statementPermission.PermissionFlag!) == true);
        }
        throw new ArgumentException(message: "First, you need to load the navigation properties.");
    }
    public override int GetHashCode() => Id;
    private bool IsNavigationProperties() => PermissionAction is not null && PermissionEntity is not null;
    #endregion
}