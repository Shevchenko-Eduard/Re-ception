using Application.Dto.Internal;

namespace Application.Dto.Output;

public static class UserDto
{
    public record UserRequestDto(
        string Id,
        string UserName,
        string? Phone,
        string Email,
        DateOnly DateOfBirth,
        byte GenderId,
        EmployeeDto.Employee? EmployeeDto,
        GuestDto.Guest? GuestDto
    );
}