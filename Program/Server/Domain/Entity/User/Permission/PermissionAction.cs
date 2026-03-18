using Domain.Abstract;

namespace Domain.Entity.User.Permission;

public sealed class PermissionAction : StatusWithParentsObjectsAbstract<PermissionAction>
{
    #region Constructors
    private PermissionAction(byte id, string name) : base(id, name) { }
    private PermissionAction(byte id, string name, params IEnumerable<PermissionAction> parents) : base(id, name) { Parents = parents; }
    #endregion
    #region Default objects
    /// <summary>
    /// Создавать
    /// </summary>
    public static readonly PermissionAction Create = new(0, nameof(Create));
    /// <summary>
    /// Читать
    /// </summary>
    public static readonly PermissionAction Read = new(1, nameof(Read));
    /// <summary>
    /// Редактировать
    /// </summary>
    public static readonly PermissionAction Update = new(2, nameof(Update));
    /// <summary>
    /// Удалять
    /// </summary>
    public static readonly PermissionAction Delete = new(3, nameof(Delete));
    /// <summary>
    /// Все CRUD операции
    /// </summary>
    public static readonly PermissionAction Manage = new(4, nameof(Manage), Create, Read, Update, Delete);
    /// <summary>
    /// Выгружать
    /// </summary>
    public static readonly PermissionAction Export = new(5, nameof(Export), Read);
    /// <summary>
    /// Загружать
    /// </summary>
    public static readonly PermissionAction Import = new(6, nameof(Import), Create);
    #endregion
}