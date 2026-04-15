using Domain.Entity.Employee;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.EmployeeRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.EmployeeRepository;

public class EfEmployeeRepository(ProgramContext context) : IEmployeeRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(Employee entity)
    {
        await _context.Employees.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<Employee, bool>> predicate)
    {
        return await _context.Employees.CountAsync(predicate);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Employees.Where(e => e.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<Employee, bool>> predicate)
    {
        return await _context.Employees.AnyAsync(predicate);
    }

    public async Task<IEnumerable<Employee>?> FindAsync(Expression<Func<Employee, bool>> specification)
    {
        return await _context.Employees.Where(specification).ToListAsync();
    }

    public async Task<Employee?> FirstAsync(Expression<Func<Employee, bool>> predicate)
    {
        return await _context.Employees.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task UpdateAsync(Employee entity)
    {
        _context.Employees.Update(entity);
    }
}