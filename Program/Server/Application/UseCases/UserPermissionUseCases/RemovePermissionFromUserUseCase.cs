using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.UseCases.UserPermissionUseCases;

public class RemovePermissionFromUserUseCase(
    IUserPermissionRepository userPermissionRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<UserPermissionDto.Remove>
{
    private readonly IUserPermissionRepository _userPermissionRepository = userPermissionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.User, PermissionFlag.Super);

    public async Task Execute(UserPermissionDto.Remove input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to remove permissions from users");
        }

        await _userPermissionRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
