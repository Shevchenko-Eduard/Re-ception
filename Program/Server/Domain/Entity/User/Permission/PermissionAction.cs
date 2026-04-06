using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class PermissionAction : StatusWithParentsObjectsAbstract<PermissionAction>
{
    #region Constructors
    private PermissionAction(string name) : base(name) { }
    private PermissionAction(string name, params IEnumerable<PermissionAction> parents) : base(name) { Parents = parents; }
    #endregion
    #region Default objects
    /// <summary>
    /// Создавать
    /// </summary>
    public static readonly PermissionAction Create = new(nameof(Create));
    /// <summary>
    /// Читать
    /// </summary>
    public static readonly PermissionAction Read = new(nameof(Read));
    /// <summary>
    /// Редактировать
    /// </summary>
    public static readonly PermissionAction Update = new(nameof(Update));
    /// <summary>
    /// Удалять
    /// </summary>
    public static readonly PermissionAction Delete = new(nameof(Delete));
    /// <summary>
    /// Все CRUD операции
    /// </summary>
    public static readonly PermissionAction Manage = new(nameof(Manage), Create, Read, Update, Delete);
    /// <summary>
    /// Выгружать
    /// </summary>
    public static readonly PermissionAction Export = new(nameof(Export), Read);
    /// <summary>
    /// Загружать
    /// </summary>
    public static readonly PermissionAction Import = new(nameof(Import), Create);
    public static readonly PermissionAction Super = new(nameof(Super), All);
    #endregion
    #region Navigation properties
    public ICollection<Permission>? Permissions { get; private set; }
    #endregion
}