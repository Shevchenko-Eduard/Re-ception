using Application.Dto.Input;

namespace Application.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(UserDto.RegisterModel model);
    Task LoginAsync(UserDto.LoginModel model);
    Task LogoutAsync();
}