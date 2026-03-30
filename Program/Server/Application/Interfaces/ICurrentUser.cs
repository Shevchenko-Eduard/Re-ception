using Domain.Entity.Employee;
using Domain.Entity.Guest;
using Domain.Entity.User;

namespace Application.Interfaces;

public interface ICurrentUser
{
    Task<User> GetUserAsync();
    Task<Guid> GetUserIdAsync();
    Task SetUserIdAsync(Guid id);
    Task ClearAsync();
    Task<Guest?> GetGuestAsync();
    Task<Employee?> GetEmployeeAsync();
}