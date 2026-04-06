using Domain.Interfaces;

namespace Application.Dto.Input;

public static class UserDto
{
    public record RegisterModel(
        string? Phone,
        string Email,
        string Password);

    public record LoginModel(
        string? Phone,
        string? Email,
        string Password);

    public record RegisterDto(
        //user
        string UserName,
        DateOnly DateOfBirth,
        byte GenderId,
        string Password,
        string Email,
        IClock Clock,
        string? Phone = null,
        EmployeeDto.Employee? Employee = null,
        GuestDto.Guest? Guest = null
    )
    {
        public RegisterModel GetRegisterModel() => new(
            Phone: Phone,
            Email: Email,
            Password: Password
        );
        public Domain.Entity.User.User GetUser() => new(
            userName: UserName,
            dateOfBirth: DateOfBirth,
            clock: Clock,
            genderId: GenderId
        );
        public Domain.Entity.Guest.Guest GetGuest(Guid userId) => new(
            userId: userId,
            clock: Clock
        );
        public Domain.Entity.Employee.Employee GetEmployee(Guid userId)
        {
            if (Employee is null)
            {
                throw new ArgumentException();
            }
            return new(
            hotelId: Employee.HotelId,
            userId: userId,
            firstName: Employee.FirstName,
            lastName: Employee.LastName,
            hireDate: Employee.HireDate,
            clock: Clock
        );
        }
    }

    public record User(
        string UserName,
        string Email,
        DateOnly DateOfBirth,
        IClock Clock,
        byte GenderId,
        string Password,
        string? Phone = null);

    public record Update(
        Guid Id,
        string? UserName = null,
        DateOnly? DateOfBirth = null,
        byte? GenderId = null)
    {
        public Domain.Entity.User.User GetUpdateUser(Domain.Entity.User.User user)
        {
            if (UserName is not null)
            {
                user.UpdateUserName(UserName);
            }
            if (DateOfBirth is not null)
            {
                user.UpdateDateOfBirth((DateOnly)DateOfBirth);
            }
            if (GenderId is not null)
            {
                user.UpdateGenderId((byte)GenderId);
            }
            return user;
        }
    }

    public record Delete(
        Guid id);
}