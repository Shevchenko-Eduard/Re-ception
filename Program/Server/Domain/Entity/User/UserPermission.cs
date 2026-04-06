using Domain.Interfaces;

namespace Domain.Entity.User;

public sealed class UserPermission
{
    private readonly IClock _clock;
    #region Fields
    public ulong Id { get; init; }
    public Guid UserId { get; init; }
    public Guid? AuthorId { get; private set; }
    public ushort PermissionId { get; init; }
    public DateTimeOffset CreateAt { get; init; }
    #endregion
    #region Navigation properties
    public User? User
    {
        get; private set
        {
            if (value is null)
            {
                throw new ArgumentException(message: "Employee must not be equal to null.");
            }
            if (value.Id != UserId)
            {
                throw new ArgumentException(message: "The employee ID does not match the ID within the relationship.");
            }
            field = value;
        }
    }
    public User? UserAuthor
    {
        get; private set
        {
            if (AuthorId is null)
            {
                throw new ArgumentException(message: "You can't attach an author without an author ID in the link.");
            }
            if (value is null)
            {
                throw new ArgumentException(message: "Author must not be equal to null.");
            }
            if (value.Id != AuthorId)
            {
                throw new ArgumentException(message: "The author ID does not match the ID within the relationship.");
            }
            field = value;
        }
    }
    public Permission.Permission? Permission
    {
        get; private set
        {
            if (value is null)
            {
                throw new ArgumentException(message: "Permission must not be equal to null.");
            }
            if (value.Id != PermissionId)
            {
                throw new ArgumentException(message: "The permission ID does not match the ID within the relationship.");
            }
            field = value;
        }
    }
    #endregion
    #region Constructors
    public UserPermission(
        Guid userId,
        int roleId,
        IClock clock,
        Guid? whoAppointedId = null)
    {
        _clock = clock;
        UserId = userId;
        PermissionId = (ushort)roleId;
        CreateAt = _clock.Now;
        AuthorId = whoAppointedId;
    }
    #endregion
    #region Methods
    public void AddAuthor(Guid whoAppointedId)
    {
        if (AuthorId is not null)
        {
            throw new ArgumentException(message: "This permission already has an author.");
        }
        AuthorId = whoAppointedId;
    }
    #endregion
}