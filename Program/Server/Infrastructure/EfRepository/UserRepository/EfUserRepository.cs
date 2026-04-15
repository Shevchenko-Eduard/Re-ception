using Domain.Entity.User;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.UserRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.UserRepository;

public class EfUserRepository(ProgramContext context) : IUserRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(User entity)
    {
        await _context.AppUsers.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<User, bool>> predicate)
    {
        return await _context.AppUsers.CountAsync(predicate);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.AppUsers.Where(u => u.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate)
    {
        return await _context.AppUsers.AnyAsync(predicate);
    }

    public async Task<IEnumerable<User>?> FindAsync(Expression<Func<User, bool>> specification)
    {
        return await _context.AppUsers.Where(specification).ToListAsync();
    }

    public async Task<User?> FirstAsync(Expression<Func<User, bool>> predicate)
    {
        return await _context.AppUsers.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.AppUsers.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateAsync(User entity)
    {
        _context.AppUsers.Update(entity);
    }
}
