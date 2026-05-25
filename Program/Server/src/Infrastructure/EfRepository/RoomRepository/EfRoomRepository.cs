using Domain.Entity.Room;
using Domain.Exception;
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

    public async Task<decimal> GetPricePerDay(int id)
    {
        Room room = await _context.Rooms
            .Include(r => r.RoomType)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id) ?? throw new DomainExternalException();
        decimal pricePerDay = (room.PricePerDay is null
            ? room.PricePerDay
            : room.RoomType!.BasePricePerDay)
            ?? throw new SystemException();
        return pricePerDay;
    }

    public async Task UpdateAsync(Room entity) => _context.Rooms.Update(entity);

    public async Task LoadRoomStatusAsync(Room room) => await _context.Entry(room).Reference(r => r.RoomStatus).LoadAsync();
}