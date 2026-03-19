namespace Application.Dto;

public class UserRequestDto
{
    public string UserName { get; set; } = null!;
    public string? Phone { get; set; }
    public string Email { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public byte GenderId { get; set; }
    public string Password { get; set; } = null!;

    public EmployeeDto EmployeeDto { get; set; } = null!;

    public GuestDto GuestDto { get; set; } = null!;
}