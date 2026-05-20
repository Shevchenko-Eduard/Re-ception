using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.HotelRepository;

public class EfHotelTagRepository(ProgramContext context) : IHotelTagRepository
{
    private readonly ProgramContext _context = context;

    public async Task AddAsync(HotelTag entity) => await _context.HotelTags.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.HotelTags.Where(ht => ht.Id == id).ExecuteDeleteAsync();

    public async Task<HotelTag?> GetByIdAsync(int id) => await _context.HotelTags.FirstOrDefaultAsync(ht => ht.Id == id);

    public async Task UpdateAsync(HotelTag entity) => _context.HotelTags.Update(entity);
}