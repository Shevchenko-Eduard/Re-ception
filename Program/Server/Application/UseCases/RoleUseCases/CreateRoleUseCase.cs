using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Entity.User.Role;
using Domain.Interfaces.Repositories.UserRepository.Role;

namespace Application.UseCases.RoleUseCases;

public class CreateRoleUseCase(
    IAuthorization authorization,
    IUnitOfWork unitOfWork,
    IRoleRepository roleRepository) : IUseCase<RoleDto.Create>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.Role, PermissionFlag.Self);

    public async Task Execute(RoleDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        Role role = input.GetRole();
        await _roleRepository.AddAsync(role);
        await _unitOfWork.SaveChangesAsync();
    }
}
