using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomImageRepository(
    ProgramContext context) : IRoomImageRepository
{
    private readonly ProgramContext _context = context;


    public async Task AddAsync(RoomImage entity) => await _context.RoomImages.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.RoomImages.Where(h => h.Id == id).ExecuteDeleteAsync();

    public async Task<RoomImage?> GetByIdAsync(int id) => await _context.RoomImages.FirstOrDefaultAsync(h => h.Id == id);

    public async Task UpdateAsync(RoomImage entity) => _context.RoomImages.Update(entity);
}