using Application.Dto.Input;
using Application.Interfaces;

namespace Application.UseCases.UserUseCases;

public class LoginUseCase(
    IAuthService authService) : IUseCase<UserDto.LoginModel>
{
    private readonly IAuthService _authService = authService;

    public async Task Execute(UserDto.LoginModel input)
    {
        await _authService.LoginAsync(input);
    }
}