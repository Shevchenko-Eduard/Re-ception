using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.HotelRepository;

public class EfHotelHotelTagRepository(ProgramContext context) : IHotelHotelTagRepository
{
    private readonly ProgramContext _context = context;

    public async Task AddAsync(HotelHotelTag entity) => await _context.HotelHotelTags.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.HotelHotelTags.Where(ht => ht.Id == id).ExecuteDeleteAsync();

    public async Task<HotelHotelTag?> GetByIdAsync(int id) => await _context.HotelHotelTags.FirstOrDefaultAsync(ht => ht.Id == id);

    public IQueryable<HotelHotelTag> GetQueryable() => _context.HotelHotelTags.AsQueryable();
}