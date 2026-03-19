using Application.Dto;
using Application.Interfaces;

namespace Application.UseCases.AuthUseCases;

public class Auth
{
    private readonly IAuthService _authService;
    private readonly RegisterDto _registerDto;
    public Auth(
        IAuthService authService,
        RegisterDto registerDto)
    {
        _authService = authService;
        _registerDto = registerDto;
    }
    public async Task<UserRequestDto> Execute()
    {
        if (await _authService.EmailExist(_registerDto.Email))
        {
            throw new ArgumentException(message: "A user with this email already exists.");
        }
        return await _authService.Register(_registerDto);
    }
}