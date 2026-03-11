namespace Domain.Entity.Employee;

public sealed class EmployeeRole
{
    public ulong EmployeeId { get; init; }
    public Employee? Employee { get; private set; }
    public ulong WhoAppointedId { get; init; }
    public Employee? WhoAppointed { get; init; }
    public ushort RoleId { get; init; }
    public Role? Role { get; private set; }
    public DateTimeOffset CreateAt { get; init; }
    public EmployeeRole(ulong employeeId, int roleId, ulong whoAppointedId)
    {
        WhoAppointedId = whoAppointedId;
        EmployeeId = employeeId;
        RoleId = (ushort)roleId;
        CreateAt = DateTimeOffset.Now;
    }
}