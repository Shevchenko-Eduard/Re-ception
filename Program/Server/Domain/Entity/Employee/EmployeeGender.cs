using Domain.Abstract;

namespace Domain.Entity.Employee;

public sealed class EmployeeGender : StatusObjectAbstract<EmployeeGender>
{
    #region Constructors
    private EmployeeGender(byte id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly EmployeeGender Female = new(0, "Female");
    public static readonly EmployeeGender Male = new(1, "Male");
    #endregion
}