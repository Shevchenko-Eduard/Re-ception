using Domain.Entity.User.Permission;

namespace Domain.Interfaces;

public interface IUserAuthorization
{
    Task<bool> Verify(Guid userId, Permission permission);
}