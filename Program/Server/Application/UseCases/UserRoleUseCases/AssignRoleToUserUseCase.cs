using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.UseCases.UserRoleUseCases;

public class AssignRoleToUserUseCase(
    IAuthorization authorization,
    IUnitOfWork unitOfWork,
    IUserRoleRepository userRoleRepository) : IUseCase<UserRoleDto.Assign>
{
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.User, PermissionFlag.Super);

    public async Task Execute(UserRoleDto.Assign input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to assign roles to users");
        }

        UserRole userRole = input.GetUserRole();
        await _userRoleRepository.AddAsync(userRole);
        await _unitOfWork.SaveChangesAsync();
    }
}
