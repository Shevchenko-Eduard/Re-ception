using Application.Interfaces;
using Domain.Entity.User.Permission;

namespace Application.UseCases.UserUseCases;

public class LogoutUseCase(
    IAuthService authService) : IUseCase
{
    private readonly IAuthService _authService = authService;

    public Permission RequiredPermission => throw new NotImplementedException("Permission is not defined for LogoutUseCase");

    public async Task Execute()
    {
        await _authService.LogoutAsync();
    }
}