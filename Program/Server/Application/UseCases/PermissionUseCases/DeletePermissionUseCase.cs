using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository.Permission;

namespace Application.UseCases.PermissionUseCases;

public class DeletePermissionUseCase(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<PermissionDto.Delete>
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;
    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.Permission, PermissionFlag.Super);

    public async Task Execute(PermissionDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to delete permissions");
        }
        await _permissionRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
