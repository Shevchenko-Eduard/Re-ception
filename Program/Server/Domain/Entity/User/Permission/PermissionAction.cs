using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class PermissionAction : StatusWithParentsObjectsAbstract<PermissionAction>
{
    #region Constructors
    private PermissionAction(byte id, string name) : base(id, name) { }
    private PermissionAction(byte id, string name, params IEnumerable<PermissionAction> parents) : base(id, name) { Parents = parents; }
    #endregion
    #region Default objects
    public static readonly PermissionAction Super = new(0, nameof(Super), All);
    /// <summary>
    /// Создавать
    /// </summary>
    public static readonly PermissionAction Create = new(1, nameof(Create));
    /// <summary>
    /// Читать
    /// </summary>
    public static readonly PermissionAction Read = new(2, nameof(Read));
    /// <summary>
    /// Редактировать
    /// </summary>
    public static readonly PermissionAction Update = new(3, nameof(Update));
    /// <summary>
    /// Удалять
    /// </summary>
    public static readonly PermissionAction Delete = new(4, nameof(Delete));
    /// <summary>
    /// Все CRUD операции
    /// </summary>
    public static readonly PermissionAction Manage = new(5, nameof(Manage), Create, Read, Update, Delete);
    /// <summary>
    /// Выгружать
    /// </summary>
    public static readonly PermissionAction Export = new(6, nameof(Export), Read);
    /// <summary>
    /// Загружать
    /// </summary>
    public static readonly PermissionAction Import = new(7, nameof(Import), Create);
    #endregion
    #region Navigation properties
    public ICollection<Permission>? Permissions { get; private set; }
    #endregion
}