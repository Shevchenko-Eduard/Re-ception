using Domain.Entity.Employee.Role;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.EmployeeRepository.Role;

public interface IBaseEmployeeRoleRepository : IBaseCrudRepository<EmployeeRole, int>
{

}