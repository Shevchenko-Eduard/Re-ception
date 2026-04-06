using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class PermissionFlag: StatusWithParentsObjectsAbstract<PermissionFlag>
{
    #region Constructors
    private PermissionFlag(string name) : base(name) { }
    private PermissionFlag(string name, params IEnumerable<PermissionFlag> parents) : base(name) { Parents = parents; }
    #endregion
    #region Default objects
    /// <summary>
    /// Только свои объекты
    /// </summary>
    public static readonly PermissionFlag Self = new(nameof(Self));
    /// <summary>
    /// Все объекты не только свои
    /// </summary>
    public static readonly PermissionFlag Any = new(nameof(Any), Self);
    /// <summary>
    /// Абсолютно любой объект
    /// </summary>
    public static readonly PermissionFlag Super = new(nameof(Super), All);
    #endregion
    #region Navigation properties
    public ICollection<Permission>? Permissions { get; private set; }
    #endregion
}