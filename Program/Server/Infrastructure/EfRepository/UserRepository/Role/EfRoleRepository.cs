using System.Linq.Expressions;
using Domain.Interfaces.Repositories.UserRepository.Role;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.UserRepository.Role;

public class EfRoleRepository(ProgramContext context) : IRoleRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(Domain.Entity.User.Role.Role entity)
    {
        await _context.Roles.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<Domain.Entity.User.Role.Role, bool>> predicate)
    {
        return await _context.Roles.CountAsync(predicate);
    }

    public async Task DeleteAsync(int id)
    {
        await _context.Roles.Where(r => r.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<Domain.Entity.User.Role.Role, bool>> predicate)
    {
        return await _context.Roles.AnyAsync(predicate);
    }

    public async Task<IEnumerable<Domain.Entity.User.Role.Role>?> FindAsync(Expression<Func<Domain.Entity.User.Role.Role, bool>> specification)
    {
        return await _context.Roles.Where(specification).ToListAsync();
    }

    public async Task<Domain.Entity.User.Role.Role?> FirstAsync(Expression<Func<Domain.Entity.User.Role.Role, bool>> predicate)
    {
        return await _context.Roles.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Domain.Entity.User.Role.Role>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Domain.Entity.User.Role.Role?> GetByIdAsync(int id)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task UpdateAsync(Domain.Entity.User.Role.Role entity)
    {
        _context.Roles.Update(entity);
    }
}
