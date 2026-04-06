using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository.Permission;

namespace Application.UseCases.PermissionUseCases;

public class CreatePermissionUseCase(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<PermissionDto.Create>
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;
    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.Permission, PermissionFlag.Super);

    public async Task Execute(PermissionDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to create permissions");
        }
        Permission permission = input.GetPermission();
        await _permissionRepository.AddAsync(permission);
        await _unitOfWork.SaveChangesAsync();
    }
}
