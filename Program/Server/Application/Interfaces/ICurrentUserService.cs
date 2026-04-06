using Domain.Entity.Employee;
using Domain.Entity.Guest;
using Domain.Entity.User;

namespace Application.Interfaces;

public interface ICurrentUserService
{
    Task<User> GetCurrentUserAsync();
    Task<Guest?> GetCurrentGuestAsync();
    Task<Employee?> GetCurrentEmployeeAsync();
}