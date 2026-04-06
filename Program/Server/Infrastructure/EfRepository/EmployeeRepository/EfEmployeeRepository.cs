using System.Linq.Expressions;
using Domain.Entity.Employee;
using Domain.Interfaces.Repositories.EmployeeRepository;

namespace Infrastructure.EfRepository.EmployeeRepository;

public class EfEmployeeRepository : IEmployeeRepository
{
    public Task AddAsync(Employee entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(Expression<Func<Employee, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<Employee, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Employee>?> FindAsync(Expression<Func<Employee, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<Employee?> FirstAsync(Expression<Func<Employee, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Employee>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Employee?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Employee entity)
    {
        throw new NotImplementedException();
    }
}