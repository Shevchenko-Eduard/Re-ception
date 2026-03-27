using Application.Dto.Internal;
using Application.Interfaces;
using Domain.Interfaces.Repositories.EmployeeRepository;
using Domain.Interfaces.Repositories.GuestRepository;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.UseCases.AuthUseCases;

public class LogIn
{
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly Dto.Input.UserDto.LogInDto _logInDto;
    private readonly IHasher _hasher;
    public LogIn(
        IUserRepository userRepository,
        IEmployeeRepository employeeRepository,
        IGuestRepository guestRepository,
        Dto.Input.UserDto.LogInDto logInDto,
        IHasher hasher)
    {
        _hasher = hasher;
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
        _guestRepository = guestRepository;
        _logInDto = logInDto;
    }
    public async Task<Dto.Output.UserDto.UserRequestDto> Execute()
    {
        if (_logInDto.Email is null && _logInDto.Phone is null && _logInDto.UserName is null)
        {
            throw new ArgumentException(message: "Email, phone or username must be provided.");
        }
        var coincidencesUser = await _userRepository.FindAsync(u =>
            (u.Email != null && u.Email.Value == _logInDto.Email) ||
            (u.Phone != null && u.Phone.Value == _logInDto.Phone) ||
            (u.UserName != null && u.UserName == _logInDto.UserName));
        if (!coincidencesUser.Any())
        {
            throw new ArgumentException(message: "User with this email, phone or username does not exist.");
        }
        var user = coincidencesUser.First();
        if (!_hasher.Verify(_logInDto.Password, user.PasswordHash))
        {
            throw new ArgumentException(message: "Incorrect password.");
        }
        EmployeeDto.Employee? employeeDto = null;
        GuestDto.Guest? guestDto = null;
        var coincidencesEmployee = await _employeeRepository.FindAsync(e => e.UserId == user.Id);
        if (coincidencesEmployee.Any())
        {
            var employee = coincidencesEmployee.First();
            employeeDto = new
            (
                HotelId: employee.HotelId,
                FirstName: employee.FirstName,
                LastName: employee.LastName,
                Patronymic: employee.Patronymic,
                HireDate: employee.HireDate);
        }
        var coincidencesGuest = await _guestRepository.FindAsync(g => g.UserId == user.Id);
        if (coincidencesGuest.Any())
        {
            var guest = coincidencesGuest.First();
            guestDto = new();
        }
        Dto.Output.UserDto.UserRequestDto userRequestDto = new
        (
            Id: user.Id.ToString(),
            UserName: user.UserName,
            Email: user.Email.Value,
            Phone: user.Phone?.Value,
            DateOfBirth: user.DateOfBirth,
            GenderId: user.GenderId,
            EmployeeDto: employeeDto,
            GuestDto: guestDto
        );
        return userRequestDto;
    }
}