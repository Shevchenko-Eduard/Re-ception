namespace Application.Dto.Internal;

public static class EmployeeDto
{
    public record Employee(
        ushort HotelId,
        string FirstName,
        string LastName,
        string? Patronymic,
        DateTimeOffset HireDate
    );
}