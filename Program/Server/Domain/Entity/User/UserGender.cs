using Domain.Abstract;

namespace Domain.Entity.User;

public sealed class UserGender : StatusObjectAbstract<UserGender>
{
    #region Constructors
    private UserGender(byte id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly UserGender Indeterminate = new(0, "Indeterminate");
    public static readonly UserGender Female = new(1, "Female");
    public static readonly UserGender Male = new(2, "Male");
    #endregion
}