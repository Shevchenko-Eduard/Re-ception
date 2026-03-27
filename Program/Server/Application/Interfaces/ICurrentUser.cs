namespace Application.Interfaces;

public interface ICurrentUser
{
    Guid CurrentUserId { get; }
}