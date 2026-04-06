using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.UseCases.UserRoleUseCases;

public class RemoveRoleFromUserUseCase(
    IUserRoleRepository userRoleRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<UserRoleDto.Remove>
{
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.User, PermissionFlag.Super);

    public async Task Execute(UserRoleDto.Remove input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to remove roles from users");
        }

        await _userRoleRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
