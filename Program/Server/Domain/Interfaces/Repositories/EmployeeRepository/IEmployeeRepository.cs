using Domain.Entity.Employee;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.EmployeeRepository;

public interface IEmployeeRepository : IBaseCrudRepository<Employee, Guid>
{
    
}