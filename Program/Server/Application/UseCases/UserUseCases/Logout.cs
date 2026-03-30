using Application.Interfaces;

namespace Application.UseCases.UserUseCases;

public class Logout(
    IAuthService authService
)
{
    private readonly IAuthService _authService = authService;
    public async Task Execute()
    {
        await _authService.LogoutAsync();
    }
}