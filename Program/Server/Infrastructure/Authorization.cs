using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces;

namespace Infrastructure;

public class Authorization(ICurrentUser currentUser, IUserAuthorization userAuthorization) : IAuthorization
{
    private ICurrentUser _currentUser => currentUser;

    private IUserAuthorization _userAuthorization => userAuthorization;

    public Task<bool> Verify(Permission permission)
    {
        var userId = _currentUser.Id ?? throw new ArgumentNullException(nameof(_currentUser.Id));
        return _userAuthorization.Verify(userId, permission);
    }
}