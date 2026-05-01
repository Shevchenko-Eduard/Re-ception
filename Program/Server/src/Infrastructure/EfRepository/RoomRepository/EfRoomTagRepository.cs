using System.Linq.Expressions;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomTagRepository(ProgramContext context) : IRoomTagRepository
{
    private readonly ProgramContext _context = context;

    public async Task AddAsync(RoomTag entity) => await _context.RoomTags.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.RoomTags.Where(rt => rt.Id == id).ExecuteDeleteAsync();

    public async Task<RoomTag?> GetByIdAsync(int id) => await _context.RoomTags.FirstOrDefaultAsync(rt => rt.Id == id);

    public IQueryable<RoomTag> GetQueryable() => _context.RoomTags.AsQueryable();

    public async Task UpdateAsync(RoomTag entity) => _context.RoomTags.Update(entity);
}