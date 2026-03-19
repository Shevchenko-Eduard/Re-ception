using Domain.Entity;
using Domain.Entity.User;
using Microsoft.VisualBasic;

namespace Application.Dto;

public sealed class RegisterDto
{
    //User
    public string UserName { get; set; } = null!;
    public string? Phone { get; set; }
    public string Email { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public byte GenderId { get; set; }
    public string Password { get; set; } = null!;
    public UserType UserType { get; set; }

    //Employee
    public ushort HotelId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Patronymic { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public DateTimeOffset HireDate { get; set; }
    
}