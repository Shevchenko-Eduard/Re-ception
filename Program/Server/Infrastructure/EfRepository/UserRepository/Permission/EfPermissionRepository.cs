using System.Linq.Expressions;
using Domain.Interfaces.Repositories.UserRepository.Permission;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.UserRepository.Permission;

public class EfPermissionRepository(ProgramContext context) : IPermissionRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task<Domain.Entity.User.Permission.Permission?> GetByIdAsync(int id)
    {
        return await _context.Permissions.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Domain.Entity.User.Permission.Permission>> GetAllAsync()
    {
        return await _context.Permissions.ToListAsync();
    }

    public async Task<IEnumerable<Domain.Entity.User.Permission.Permission>?> FindAsync(Expression<Func<Domain.Entity.User.Permission.Permission, bool>> specification)
    {
        return await _context.Permissions.Where(specification).ToListAsync();
    }

    public async Task<Domain.Entity.User.Permission.Permission?> FirstAsync(Expression<Func<Domain.Entity.User.Permission.Permission, bool>> predicate)
    {
        return await _context.Permissions.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<Domain.Entity.User.Permission.Permission, bool>> predicate)
    {
        return await _context.Permissions.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<Domain.Entity.User.Permission.Permission, bool>> predicate)
    {
        return await _context.Permissions.CountAsync(predicate);
    }

    public async Task AddAsync(Domain.Entity.User.Permission.Permission entity)
    {
        await _context.Permissions.AddAsync(entity);
    }

    public async Task DeleteAsync(byte id)
    {
        await _context.Permissions.Where(p => p.Id == id).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Domain.Entity.User.Permission.Permission>> GetPermissionsByRolesAsync(IEnumerable<ushort> rolesId)
    {
        return await _context.Permissions
            .Include(p => p.Roles)
            .Where(p => p.Roles != null && p.Roles.Where(r =>rolesId.Contains(r.Id)) != null)
            .ToListAsync();
    }
}
