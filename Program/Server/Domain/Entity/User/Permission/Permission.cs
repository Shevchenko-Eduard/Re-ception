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
    public PermissionFlag? Action { get; private set; }
    public PermissionEntity? Entity { get; private set; }
    public PermissionFlag? Flag { get; private set; }
    public IEnumerable<User>? Users { get; private set; }
    public IEnumerable<Role.Role>? Roles { get; private set; }
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
            return Action!.Equals(permission.Action!) &&
                Entity!.Equals(permission.Entity!) &&
                (Flag?.Equals(permission.Flag!) == true);
        }
        throw new ArgumentException(message: "First, you need to load the navigation properties.");
    }
    public override int GetHashCode() => Id;
    private bool IsNavigationProperties() => Action is not null && Entity is not null;
    #endregion
}