using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.EmployeeRepository;

namespace Application.UseCases.EmployeeUseCases;

public class CreateEmployeeUseCase(
    IAuthorization authorization,
    IUnitOfWork unitOfWork,
    IEmployeeRepository employeeRepository) : IUseCase<EmployeeDto.Create>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.Employee, PermissionFlag.Self);

    public async Task Execute(EmployeeDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to create employees");
        }
        Domain.Entity.Employee.Employee employee = input.GetEmployee();
        await _employeeRepository.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();
    }
}
