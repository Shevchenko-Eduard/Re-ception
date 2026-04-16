using System.Linq.Expressions;
using Domain.Entity.User;
using Domain.Interfaces.Repositories.UserRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.UserRepository;

public class EfUserRoleRepository(ProgramContext context) : IUserRoleRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(UserRole entity)
    {
        await _context.UserRoles.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<UserRole, bool>> predicate)
    {
        return await _context.UserRoles.CountAsync(predicate);
    }

    public async Task DeleteAsync(ulong id)
    {
        await _context.UserRoles.Where(ur => ur.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<UserRole, bool>> predicate)
    {
        return await _context.UserRoles.AnyAsync(predicate);
    }

    public async Task<IEnumerable<UserRole>?> FindAsync(Expression<Func<UserRole, bool>> specification)
    {
        return await _context.UserRoles.Where(specification).ToListAsync();
    }

    public async Task<UserRole?> FirstAsync(Expression<Func<UserRole, bool>> predicate)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<UserRole>> GetAllAsync()
    {
        return await _context.UserRoles.ToListAsync();
    }

    public async Task<UserRole?> GetByIdAsync(ulong id)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Id == id);
    }

    public async Task<IEnumerable<ushort>> GetRolesIdByUserAsync(Guid userId)
    {
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync();
    }
}
