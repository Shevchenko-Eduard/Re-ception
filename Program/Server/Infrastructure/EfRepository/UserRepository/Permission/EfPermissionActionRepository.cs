using System.Linq.Expressions;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository.Permission;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.UserRepository.Permission;

public class EfPermissionActionRepository(ProgramContext context) : IPermissionActionRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task<PermissionFlag?> GetByIdAsync(int id)
    {
        return await _context.PermissionFlags.FirstOrDefaultAsync(pf => pf.Id == id);
    }

    public async Task<IEnumerable<PermissionFlag>> GetAllAsync()
    {
        return await _context.PermissionFlags.ToListAsync();
    }

    public async Task<IEnumerable<PermissionFlag>?> FindAsync(Expression<Func<PermissionFlag, bool>> specification)
    {
        return await _context.PermissionFlags.Where(specification).ToListAsync();
    }

    public async Task<PermissionFlag?> FirstAsync(Expression<Func<PermissionFlag, bool>> predicate)
    {
        return await _context.PermissionFlags.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<PermissionFlag, bool>> predicate)
    {
        return await _context.PermissionFlags.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<PermissionFlag, bool>> predicate)
    {
        return await _context.PermissionFlags.CountAsync(predicate);
    }

    public async Task<PermissionFlag?> GetByNameAsync(string name)
    {
        return await _context.PermissionFlags.FirstOrDefaultAsync(pf => pf.Name == name);
    }
}
