using Domain.Entity.Employee;
using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.Repositories.EmployeeRepository;

public interface IEmployeeEmployeePermissionRepository :
    IBaseCreateRepository<EmployeeEmployeePermission>,
    IBaseReadRepository<EmployeeEmployeePermission, ulong>,
    IBaseDeleteRepository<EmployeeEmployeePermission, ulong>
{

}