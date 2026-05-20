using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomStatusRepository(ProgramContext context) : IRoomStatusRepository
{
    private readonly ProgramContext _context = context;

    public async Task<RoomStatus?> GetByIdAsync(int id) => await _context.RoomStatuses.FirstOrDefaultAsync(rs => rs.Id == id);

    public async Task<RoomStatus?> GetByNameAsync(string name) => await _context.RoomStatuses.FirstOrDefaultAsync(rs => rs.Name == name);

}