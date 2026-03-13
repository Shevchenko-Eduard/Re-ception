using Domain.Entity.Employee.Role;

namespace Domain.Entity.Employee;

public sealed class EmployeeEmployeeRole
{
    #region Fields
    public Guid EmployeeId { get; init; }
    public Guid? AuthorId { get; init; }
    public ushort RoleId { get; init; }
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
    public EmployeeRole? Role
    {
        get; private set
        {
            if (value is null)
            {
                throw new ArgumentException(message: "Role must not be equal to null.");
            }
            if (value.Id != RoleId)
            {
                throw new ArgumentException(message: "The role ID does not match the ID within the relationship.");
            }
            field = value;
        }
    }
    #endregion
    #region Constructors
    public EmployeeEmployeeRole(
        Guid employeeId,
        int roleId)
    {
        EmployeeId = employeeId;
        RoleId = (ushort)roleId;
        CreateAt = DateTimeOffset.Now;
    }
    public EmployeeEmployeeRole(
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