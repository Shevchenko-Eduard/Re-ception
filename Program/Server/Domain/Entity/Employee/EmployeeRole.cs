namespace Domain.Entity.Employee;

public sealed class EmployeeRole
{
    public ulong EmployeeId { get; private set; }
    public Employee? Employee { get; private set; }
    public ushort RoleId { get; private set; }
    public Role? Role { get; private set; }
    public DateTimeOffset CreateAt { get; init; }
    public EmployeeRole(int employeeId, int roleId)
    {
        checked
        {
            EmployeeId = (ulong)employeeId;
            RoleId = (ushort)roleId;
        }
        CreateAt = DateTimeOffset.Now;
    }
}