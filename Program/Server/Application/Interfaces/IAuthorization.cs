using Domain.Entity.User.Permission;

namespace Application.Interfaces;

public interface IAuthorization
{
    Task<bool> Verify(Permission permission);
}