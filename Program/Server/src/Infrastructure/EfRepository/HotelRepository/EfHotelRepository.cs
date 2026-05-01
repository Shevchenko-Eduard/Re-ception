using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.HotelRepository;

public class EfHotelRepository(ProgramContext context) : IHotelRepository
{
    private readonly ProgramContext _context = context;

    public async Task AddAsync(Hotel entity) => await _context.Hotels.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.Hotels.Where(h => h.Id == id).ExecuteDeleteAsync();

    public async Task<Hotel?> GetByIdAsync(int id) => await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);

    public IQueryable<Hotel> GetQueryable() => _context.Hotels.AsQueryable();

    public async Task UpdateAsync(Hotel entity) => _context.Hotels.Update(entity);
}