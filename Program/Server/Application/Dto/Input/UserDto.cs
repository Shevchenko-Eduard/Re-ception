using Domain.Entity.User;

namespace Application.Dto.Input;

public static class UserDto
{
    public record RegisterDto(
        //User
        string UserName,
        string? Phone,
        string Email,
        DateOnly DateOfBirth,
        byte GenderId,
        string Password,
        UserType UserType,
        //Employee
        ushort HotelId,
        string FirstName,
        string LastName,
        string? Patronymic,
        DateTimeOffset HireDate);
    public record LogInDto(
        string? UserName,
        string? Email,
        string? Phone,
        string Password);
}