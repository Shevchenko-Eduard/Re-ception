using Domain.Abstract;

namespace Domain.Entity.User;

public sealed class UserGender : StatusObjectAbstract<UserGender>
{
    #region Constructors
    private UserGender(string name) : base(name) { }
    #endregion
    #region Default objects
    public static readonly UserGender Indeterminate = new("Indeterminate");
    public static readonly UserGender Female = new("Female");
    public static readonly UserGender Male = new("Male");
    #endregion
    #region Navigation properties
    public ICollection<User>? Users { get; private set; }
    #endregion
}