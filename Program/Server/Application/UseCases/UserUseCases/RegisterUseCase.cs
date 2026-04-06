using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Employee;
using Domain.Entity.Guest;
using Domain.Entity.User;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.EmployeeRepository;
using Domain.Interfaces.Repositories.GuestRepository;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.UseCases.UserUseCases;

public class RegisterUseCase(
    IUnitOfWork unitOfWorld,
    IUserRepository userRepository,
    IEmployeeRepository employeeRepository,
    IGuestRepository guestRepository,
    IAuthService authService) : IUseCase<UserDto.RegisterDto>
{
    private readonly IUnitOfWork _unitOfWorld = unitOfWorld;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IGuestRepository _guestRepository = guestRepository;
    private readonly IAuthService _authService = authService;

    public Permission RequiredPermission => throw new NotImplementedException();

    public async Task Execute(UserDto.RegisterDto input)
    {
        await _unitOfWorld.BeginTransactionAsync();
        try
        {
            UserDto.RegisterModel registerModel = input.GetRegisterModel();
            await _authService.RegisterAsync(registerModel);

            User user = input.GetUser();
            await _userRepository.AddAsync(user);

            if (input.Guest is not null)
            {
                Guest guest = input.GetGuest(user.Id);
                await _guestRepository.AddAsync(guest);
            }

            if (input.Employee is not null)
            {
                Employee employee = input.GetEmployee(user.Id);
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