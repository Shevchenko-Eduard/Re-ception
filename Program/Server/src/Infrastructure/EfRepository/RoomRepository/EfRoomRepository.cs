using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomRepository(ProgramContext context) : IRoomRepository
{
    private readonly ProgramContext _context = context;

    public async Task AddAsync(Room entity) => await _context.Rooms.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.Rooms.Where(r => r.Id == id).ExecuteDeleteAsync();

    public async Task<Room?> GetByIdAsync(int id) => await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);

    public IQueryable<Room> GetQueryable() => _context.Rooms.AsQueryable();

    public async Task UpdateAsync(Room entity) => _context.Rooms.Update(entity);
}