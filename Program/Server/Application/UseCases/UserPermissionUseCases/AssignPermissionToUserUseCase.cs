using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.UseCases.UserPermissionUseCases;

public class AssignPermissionToUserUseCase(
    IAuthorization authorization,
    IUnitOfWork unitOfWork,
    IUserPermissionRepository userPermissionRepository) : IUseCase<UserPermissionDto.Assign>
{
    private readonly IUserPermissionRepository _userPermissionRepository = userPermissionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.User, PermissionFlag.Super);

    public async Task Execute(UserPermissionDto.Assign input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to assign permissions to users");
        }

        UserPermission userPermission = input.GetUserPermission();
        await _userPermissionRepository.AddAsync(userPermission);
        await _unitOfWork.SaveChangesAsync();
    }
}
