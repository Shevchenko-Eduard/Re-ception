using Domain.Entity.Employee.Permission;

namespace Domain.Entity.Employee;

public sealed class EmployeeEmployeePermission
{
    #region Fields
    public ulong Id { get; init; }
    public Guid EmployeeId { get; init; }
    public Guid? AuthorId { get; init; }
    public ushort PermissionId { get; init; }
    public DateTimeOffset CreateAt { get; init; }
    #endregion
    #region Navigation properties
    public Employee? Employee
    {
        get; private set
        {
            if (value is null)
            {
                throw new ArgumentException(message: "Employee must not be equal to null.");
            }
            if (value.Id != EmployeeId)
            {
                throw new ArgumentException(message: "The employee ID does not match the ID within the relationship.");
            }
            field = value;
        }
    }
    public Employee? Author
    {
        get; private set
        {
            if (AuthorId is null)
            {
                throw new ArgumentException(message: "You can't attach an author without an author ID in the link.");
            }
            if (value is null)
            {
                throw new ArgumentException(message: "Author must not be equal to null.");
            }
            if (value.Id != AuthorId)
            {
                throw new ArgumentException(message: "The author ID does not match the ID within the relationship.");
            }
            field = value;
        }
    }
    public EmployeePermission? Permission
    {
        get; private set
        {
            if (value is null)
            {
                throw new ArgumentException(message: "Permission must not be equal to null.");
            }
            if (value.Id != PermissionId)
            {
                throw new ArgumentException(message: "The permission ID does not match the ID within the relationship.");
            }
            field = value;
        }
    }
    #endregion
    #region Constructors
    public EmployeeEmployeePermission(
        Guid employeeId,
        int roleId)
    {
        EmployeeId = employeeId;
        PermissionId = (ushort)roleId;
        CreateAt = DateTimeOffset.Now;
    }
    public EmployeeEmployeePermission(
        Guid employeeId,
        Guid whoAppointedId,
        int roleId) : this(
            employeeId: employeeId,
            roleId: roleId)
    {
        AuthorId = whoAppointedId;
    }
    #endregion
}