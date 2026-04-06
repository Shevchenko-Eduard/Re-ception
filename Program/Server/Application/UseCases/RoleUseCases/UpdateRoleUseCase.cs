using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Entity.User.Role;
using Domain.Interfaces.Repositories.UserRepository.Role;

namespace Application.UseCases.RoleUseCases;

public class UpdateRoleUseCase(
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<RoleDto.Update>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Update, PermissionEntity.Role, PermissionFlag.Self);

    public async Task Execute(RoleDto.Update input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to update roles");
        }
        
        Role role = await _roleRepository.GetByIdAsync(input.Id)
            ?? throw new ArgumentException("Role with the specified ID not found");
        Role updatedRole = input.GetUpdateRole(role);
        await _roleRepository.UpdateAsync(updatedRole);
        await _unitOfWork.SaveChangesAsync();
    }
}
