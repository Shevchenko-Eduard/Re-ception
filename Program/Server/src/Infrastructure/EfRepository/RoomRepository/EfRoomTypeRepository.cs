using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomTypeRepository(ProgramContext context) : IRoomTypeRepository
{
    private readonly ProgramContext _context = context;

    public async Task AddAsync(RoomType entity) => await _context.RoomTypes.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.RoomTypes.Where(rt => rt.Id == id).ExecuteDeleteAsync();

    public async Task<RoomType?> GetByIdAsync(int id) => await _context.RoomTypes.FirstOrDefaultAsync(rt => rt.Id == id);

    public IQueryable<RoomType> GetQueryable() => _context.RoomTypes.AsQueryable();

    public async Task UpdateAsync(RoomType entity) => _context.RoomTypes.Update(entity);
}