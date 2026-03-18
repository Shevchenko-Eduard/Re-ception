using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class Permission : EnumObjectAbstract<Permission>
{
    #region Fields
    public byte ActionId { get; init; }
    public byte TableId { get; init; }
    #endregion
    #region Navigation properties
    public PermissionFlag? Action { get; private set; }
    public PermissionEntity? Table { get; private set; }
    public IEnumerable<Employee.Employee>? Employees { get; private set; }
    public IEnumerable<Role.Role>? Roles { get; private set; }
    #endregion
    #region Constructors
    private Permission(byte id, byte actionId, byte tableId) : base(id)
    {
        ActionId = actionId;
        TableId = tableId;
    }
    #endregion
    #region Default objects
    #endregion
}