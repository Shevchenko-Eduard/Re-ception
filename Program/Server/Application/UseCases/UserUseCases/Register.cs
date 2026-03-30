using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Employee;
using Domain.Entity.Guest;
using Domain.Entity.User;
using Domain.Interfaces.Repositories.EmployeeRepository;
using Domain.Interfaces.Repositories.GuestRepository;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.UseCases.AuthUseCases;

public class Register(
    IUnitOfWork unitOfWorld,
    IUserRepository userRepository,
    IEmployeeRepository employeeRepository,
    IGuestRepository guestRepository,
    IAuthService authService)
{
    private readonly IUnitOfWork _unitOfWorld = unitOfWorld;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IGuestRepository _guestRepository = guestRepository;
    private readonly IAuthService _authService = authService;

    public async Task Execute(UserDto.RegisterDto registerDto)
    {
        await _unitOfWorld.BeginTransactionAsync();
        try
        {
            UserDto.RegisterModel registerModel = registerDto.GetRegisterModel();
            await _authService.RegisterAsync(registerModel);

            User user = registerDto.GetUser();
            await _userRepository.AddAsync(user);

            if (registerDto.Guest is not null)
            {
                Guest guest = registerDto.GetGuest(user.Id);
                await _guestRepository.AddAsync(guest);
            }

            if (registerDto.Employee is not null)
            {
                Employee employee = registerDto.GetEmployee(user.Id);
                await _employeeRepository.AddAsync(employee);
            }

            await _unitOfWorld.SaveChangesAsync();
            await _unitOfWorld.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWorld.RollbackTransactionAsync();
            throw;
        }
    }
}