using Domain.Abstract;
using Domain.Entity.Employee.Role;

namespace Domain.Entity.Employee.Permission;

public class EmployeePermission : EnumObjectAbstract<EmployeePermission>
{
    #region Fields
    public byte ActionId { get; init; }
    public byte TableId { get; init; }
    #endregion
    #region Navigation properties
    public EmployeePermissionAction? Action { get; private set; }
    public EmployeePermissionTable? Table { get; private set; }
    public IEnumerable<Employee>? Employees { get; private set; }
    public IEnumerable<EmployeeRole>? Roles { get; private set; }
    #endregion
    #region Constructors
    private EmployeePermission(byte id, byte actionId, byte tableId) : base(id)
    {
        ActionId = actionId;
        TableId = tableId;
    }
    #endregion
    #region Default objects
    #endregion
}