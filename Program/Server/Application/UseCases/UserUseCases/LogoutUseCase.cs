using Application.Interfaces;

namespace Application.UseCases.UserUseCases;

public class LogoutUseCase(
    IAuthService authService) : IUseCase
{
    private readonly IAuthService _authService = authService;
    public async Task Execute()
    {
        await _authService.LogoutAsync();
    }
}