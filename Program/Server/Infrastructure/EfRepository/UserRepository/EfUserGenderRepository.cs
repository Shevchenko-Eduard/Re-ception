using Domain.Entity.User;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.UserRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.UserRepository;

public class EfUserGenderRepository(ProgramContext context) : IUserGenderRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task<UserGender?> GetByIdAsync(int id)
    {
        return await _context.UserGenders.FirstOrDefaultAsync(ug => ug.Id == id);
    }

    public async Task<IEnumerable<UserGender>> GetAllAsync()
    {
        return await _context.UserGenders.ToListAsync();
    }

    public async Task<IEnumerable<UserGender>?> FindAsync(Expression<Func<UserGender, bool>> specification)
    {
        return await _context.UserGenders.Where(specification).ToListAsync();
    }

    public async Task<UserGender?> FirstAsync(Expression<Func<UserGender, bool>> predicate)
    {
        return await _context.UserGenders.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<UserGender, bool>> predicate)
    {
        return await _context.UserGenders.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<UserGender, bool>> predicate)
    {
        return await _context.UserGenders.CountAsync(predicate);
    }

    public async Task<UserGender?> GetByNameAsync(string name)
    {
        return await _context.UserGenders.FirstOrDefaultAsync(ug => ug.Name == name);
    }
}
