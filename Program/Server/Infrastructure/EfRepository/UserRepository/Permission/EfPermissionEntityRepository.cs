using System.Linq.Expressions;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository.Permission;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.UserRepository.Permission;

public class EfPermissionEntityRepository(ProgramContext context) : IPermissionEntityRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task<PermissionEntity?> GetByIdAsync(int id)
    {
        return await _context.PermissionEntities.FirstOrDefaultAsync(pe => pe.Id == id);
    }

    public async Task<IEnumerable<PermissionEntity>> GetAllAsync()
    {
        return await _context.PermissionEntities.ToListAsync();
    }

    public async Task<IEnumerable<PermissionEntity>?> FindAsync(Expression<Func<PermissionEntity, bool>> specification)
    {
        return await _context.PermissionEntities.Where(specification).ToListAsync();
    }

    public async Task<PermissionEntity?> FirstAsync(Expression<Func<PermissionEntity, bool>> predicate)
    {
        return await _context.PermissionEntities.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<PermissionEntity, bool>> predicate)
    {
        return await _context.PermissionEntities.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<PermissionEntity, bool>> predicate)
    {
        return await _context.PermissionEntities.CountAsync(predicate);
    }

    public async Task<PermissionEntity?> GetByNameAsync(string name)
    {
        return await _context.PermissionEntities.FirstOrDefaultAsync(pe => pe.Name == name);
    }
}
