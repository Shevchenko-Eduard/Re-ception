using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository.Role;

namespace Application.UseCases.RoleUseCases;

public class DeleteRoleUseCase(
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<RoleDto.Delete>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.Role, PermissionFlag.Self);

    public async Task Execute(RoleDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        await _roleRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
