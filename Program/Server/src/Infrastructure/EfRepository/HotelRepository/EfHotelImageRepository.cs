using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.HotelRepository;

public class EfHotelImageRepository(
    ProgramContext context) : IHotelImageRepository
{
    private readonly ProgramContext _context = context;


    public async Task AddAsync(HotelImage entity) => await _context.HotelImages.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.HotelImages.Where(h => h.Id == id).ExecuteDeleteAsync();

    public async Task<HotelImage?> GetByIdAsync(int id) => await _context.HotelImages.FirstOrDefaultAsync(h => h.Id == id);

    public async Task UpdateAsync(HotelImage entity) => _context.HotelImages.Update(entity);
}