using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomRoomTagRepository(ProgramContext context) : IRoomRoomTagRepository
{
    private readonly ProgramContext _context = context;

    public async Task AddAsync(RoomRoomTag entity) => await _context.RoomRoomTags.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.RoomRoomTags.Where(ht => ht.Id == id).ExecuteDeleteAsync();

    public async Task<RoomRoomTag?> GetByIdAsync(int id) => await _context.RoomRoomTags.FirstOrDefaultAsync(ht => ht.Id == id);

}