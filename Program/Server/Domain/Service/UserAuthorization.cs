using Domain.Entity.User.Permission;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.UserRepository;
using Domain.Interfaces.Repositories.UserRepository.Permission;

namespace Domain.Service;

public class UserAuthorization(
    IUserRepository userRepository,
    IPermissionRepository permissionRepository,
    IUserRoleRepository userRoleRepository,
    IUserPermissionRepository userPermissionRepository) : IUserAuthorization
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserPermissionRepository _userPermissionRepository = userPermissionRepository;
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    public async Task<bool> Verify(Guid userId, Permission permission)
    {
        _ = await _userRepository.GetByIdAsync(userId)
            ?? throw new ArgumentException();
        IEnumerable<Permission> userPermissions = await _userPermissionRepository.GetPermissionsByUserAsync(userId);
        if (await CheckingPermissions(userPermissions, permission)) { return true; }
        IEnumerable<ushort> rolesIdUser = await _userRoleRepository.GetRolesIdByUserAsync(userId);
        IEnumerable<Permission> rolesPermissions = await _permissionRepository.GetPermissionsByRolesAsync(rolesIdUser);
        return await CheckingPermissions(rolesPermissions, permission);
        
    }
    private static async Task<bool> CheckingPermissions(IEnumerable<Permission> verificationPermissions, Permission statementPermission)
    {
        foreach (Permission permission in verificationPermissions)
        {
            if (permission.Equals(statementPermission))
            {
                return true;
            }
        }
        return false;
    }
}
