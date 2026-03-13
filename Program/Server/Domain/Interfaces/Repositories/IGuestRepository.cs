using Domain.Entity.Guest;

namespace Domain.Interfaces.Repositories;

public interface IGuestRepository
{
    Task AddAsync(Guest guest);
	Task UpdateAsync(Guest guest);
	Task DeleteAsync(ulong id);
    Task<Guest?> GetByIdAsync(ulong id);
}