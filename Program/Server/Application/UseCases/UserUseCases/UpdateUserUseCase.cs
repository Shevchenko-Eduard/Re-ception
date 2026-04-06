using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.UseCases.UserUseCases;

public class UpdateUserUseCase(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<UserDto.Update>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;
    public Permission RequiredPermission => new(PermissionAction.Update, PermissionEntity.User, PermissionFlag.Self);

    public async Task Execute(UserDto.Update input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to update users");
        }
        User user = await _userRepository.GetByIdAsync(input.Id)
            ?? throw new ArgumentException("User with the specified ID not found");
        User updatedUser = input.GetUpdateUser(user);
        await _userRepository.UpdateAsync(updatedUser);
        await _unitOfWork.SaveChangesAsync();
    }
}
