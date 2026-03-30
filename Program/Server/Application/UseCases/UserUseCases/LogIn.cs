using Application.Dto.Input;
using Application.Interfaces;

namespace Application.UseCases.AuthUseCases;

public class LogIn
{
    private readonly IAuthService _authService;
    public LogIn(
        IAuthService authService)
    {
        _authService = authService;
    }
    public async Task Execute(UserDto.LoginModel loginModel)
    {
        await _authService.LoginAsync(loginModel);
    }
}