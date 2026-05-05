namespace Application.Interfaces;

public interface ICurrentUser
{
    string? Id { get; }
    bool IsAuthenticated { get; }
}