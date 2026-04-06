using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.UseCases.UserUseCases;

public class DeleteUserUseCase(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<UserDto.Delete>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;
    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.User, PermissionFlag.Self);

    public async Task Execute(UserDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        await _userRepository.DeleteAsync(input.id);
        await _unitOfWork.SaveChangesAsync();
    }
}
