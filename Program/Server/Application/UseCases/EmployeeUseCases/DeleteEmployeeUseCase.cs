using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.EmployeeRepository;

namespace Application.UseCases.EmployeeUseCases;

public class DeleteEmployeeUseCase(
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<EmployeeDto.Delete>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.Employee, PermissionFlag.Self);

    public async Task Execute(EmployeeDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to delete employees");
        }
        await _employeeRepository.DeleteAsync(input.id);
        await _unitOfWork.SaveChangesAsync();
    }
}
