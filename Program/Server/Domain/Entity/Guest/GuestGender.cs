using Domain.Abstract;

namespace Domain.Entity.Guest;

public sealed class GuestGender : StatusObjectAbstract<GuestGender>
{
    #region Constructors
    private GuestGender(byte id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly GuestGender Indeterminate = new(0, "Indeterminate");
    public static readonly GuestGender Female = new(1, "Female");
    public static readonly GuestGender Male = new(2, "Male");
    #endregion
}