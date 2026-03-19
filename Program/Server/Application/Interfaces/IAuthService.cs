using Application.Dto;
using Domain.Interfaces.Repositories.EmployeeRepository;
using Domain.Interfaces.Repositories.GuestRepository;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.Interfaces;

public interface IAuthService
{
    IUserRepository UserRepository { get; }
    IGuestRepository GuestRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IHasher Hasher { get; }
    Task<bool> EmailExist(string email);
    Task<UserRequestDto> Register(RegisterDto registerDto);
}