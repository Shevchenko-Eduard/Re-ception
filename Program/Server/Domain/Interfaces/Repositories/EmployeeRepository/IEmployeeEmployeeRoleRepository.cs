using Domain.Entity.Employee;
using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.Repositories.EmployeeRepository;

public interface IEmployeeEmployeeRoleRepository :
    IBaseCreateRepository<EmployeeEmployeeRole>,
    IBaseReadRepository<EmployeeEmployeeRole, ulong>,
    IBaseDeleteRepository<EmployeeEmployeeRole, ulong>
{

}