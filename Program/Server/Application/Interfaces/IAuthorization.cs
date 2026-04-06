using Domain.Entity.User.Permission;
using Domain.Interfaces;

namespace Application.Interfaces;

public interface IAuthorization
{
    ICurrentUser _currentUser { get; }
    IUserAuthorization _userAuthorization { get; }
    Task<bool> Verify(Permission permission);
}