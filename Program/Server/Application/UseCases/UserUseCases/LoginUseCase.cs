using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;

namespace Application.UseCases.UserUseCases;

public class LoginUseCase(
    IAuthService authService) : IUseCase<UserDto.LoginModel>
{
    private readonly IAuthService _authService = authService;

    public Permission RequiredPermission => throw new NotImplementedException("Permission is not defined for LoginUseCase");

    public async Task Execute(UserDto.LoginModel input)
    {
        await _authService.LoginAsync(input);
    }
}