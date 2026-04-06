using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.EmployeeRepository;

namespace Application.UseCases.EmployeeUseCases;

public class UpdateEmployeeUseCase(
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<EmployeeDto.Update>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Update, PermissionEntity.Employee, PermissionFlag.Self);

    public async Task Execute(EmployeeDto.Update input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to update employees");
        }
        Domain.Entity.Employee.Employee employee = await _employeeRepository.GetByIdAsync(input.Id)
            ?? throw new ArgumentException("Employee with the specified ID not found");
        Domain.Entity.Employee.Employee updatedEmployee = input.GetUpdateEmployee(employee);
        await _employeeRepository.UpdateAsync(updatedEmployee);
        await _unitOfWork.SaveChangesAsync();
    }
}
