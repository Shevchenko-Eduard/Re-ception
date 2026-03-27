using Application.Dto.Internal;
using Application.Interfaces;
using Domain.Entity.Employee;
using Domain.Entity.Guest;
using Domain.Entity.User;
using Domain.Interfaces.Repositories.EmployeeRepository;
using Domain.Interfaces.Repositories.GuestRepository;
using Domain.Interfaces.Repositories.UserRepository;

namespace Application.UseCases.AuthUseCases;

public class Register
{
    private readonly IUnitOfWork _unitOfWorld;
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly Dto.Input.UserDto.RegisterDto _registerDto;
    private readonly IHasher _hasher;
    public Register(
        IUnitOfWork unitOfWorld,
        IUserRepository userRepository,
        IEmployeeRepository employeeRepository,
        IGuestRepository guestRepository,
        Dto.Input.UserDto.RegisterDto registerDto,
        IHasher hasher)
    {
        _hasher = hasher;
        _unitOfWorld = unitOfWorld;
        _registerDto = registerDto;
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
        _guestRepository = guestRepository;
    }
    public async Task<Dto.Output.UserDto.UserRequestDto> Execute()
    {
        await _unitOfWorld.BeginTransactionAsync();
        try
        {
            if (await _userRepository.EmailExist(_registerDto.Email))
            {
                throw new ArgumentException(message: "A user with this email already exists.");
            }
            User user = new(
                userName: _registerDto.UserName,
                email: new Domain.Entity.Email(_registerDto.Email),
                dateOfBirth: _registerDto.DateOfBirth,
                passwordHash: _hasher.Hash(_registerDto.Password),
                clock: _unitOfWorld.Clock,
                genderId: _registerDto.GenderId);
            if (_registerDto.Phone is not null)
            {
                user.AddPhone(new Domain.Entity.Phone(_registerDto.Phone));
            }

            GuestDto.Guest? guestDto = null;
            EmployeeDto.Employee? employeeDto = null;

            if (_registerDto.UserType == UserType.Guest)
            {
                Guest guest = new(
                    userId: user.Id,
                    clock: _unitOfWorld.Clock);
                guestDto = new();
                await _guestRepository.AddAsync(guest);
                await _unitOfWorld.SaveChangesAsync();
            }
            if (_registerDto.UserType == UserType.Employee)
            {
                Employee employee = new(
                    hotelId: _registerDto.HotelId,
                    userId: user.Id,
                    firstName: _registerDto.FirstName,
                    lastName: _registerDto.LastName,
                    hireDate: _registerDto.HireDate,
                    clock: _unitOfWorld.Clock);
                if (_registerDto.Patronymic is not null)
                {
                    employee.AddPatronymic(_registerDto.Patronymic);
                }
                employeeDto = new(
                    HotelId: employee.HotelId,
                    FirstName: employee.FirstName,
                    LastName: employee.LastName,
                    Patronymic: employee.Patronymic,
                    HireDate: employee.HireDate
                );
                await _employeeRepository.AddAsync(employee);
                await _unitOfWorld.SaveChangesAsync();
            }
            Dto.Output.UserDto.UserRequestDto userRequestDto = new(
                Id: user.Id.ToString(),
                UserName: user.UserName,
                Email: user.Email.Value,
                Phone: user.Phone?.Value,
                DateOfBirth: user.DateOfBirth,
                GenderId: user.GenderId,
                EmployeeDto: employeeDto,
                GuestDto: guestDto
            );
            await _userRepository.AddAsync(user);
            await _unitOfWorld.SaveChangesAsync();
            await _unitOfWorld.CommitTransactionAsync();
            return userRequestDto;
        }
        catch
        {
            await _unitOfWorld.RollbackTransactionAsync();
            throw;
        }
    }
}