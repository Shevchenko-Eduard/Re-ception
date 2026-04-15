using Domain.Entity.User;
using Domain.Entity.User.Permission;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.UserRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.UserRepository;

public class EfUserPermissionRepository(ProgramContext context) : IUserPermissionRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(UserPermission entity)
    {
        await _context.UserPermissions.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<UserPermission, bool>> predicate)
    {
        return await _context.UserPermissions.CountAsync(predicate);
    }

    public async Task DeleteAsync(ulong id)
    {
        await _context.UserPermissions.Where(up => up.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<UserPermission, bool>> predicate)
    {
        return await _context.UserPermissions.AnyAsync(predicate);
    }

    public async Task<IEnumerable<UserPermission>?> FindAsync(Expression<Func<UserPermission, bool>> specification)
    {
        return await _context.UserPermissions.Where(specification).ToListAsync();
    }

    public async Task<UserPermission?> FirstAsync(Expression<Func<UserPermission, bool>> predicate)
    {
        return await _context.UserPermissions.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<UserPermission>> GetAllAsync()
    {
        return await _context.UserPermissions.ToListAsync();
    }

    public async Task<UserPermission?> GetByIdAsync(ulong id)
    {
        return await _context.UserPermissions.FirstOrDefaultAsync(up => up.Id == id);
    }

    public async Task<IEnumerable<Domain.Entity.User.Permission.Permission>> GetPermissionsByUserAsync(Guid userId)
    {
        return await _context.UserPermissions
            .Where(up => up.UserId == userId && up.Permission != null)
            .Select(up => up.Permission!)
            .ToListAsync();
    }
}
