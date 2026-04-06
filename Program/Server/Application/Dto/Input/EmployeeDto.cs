using Domain.Interfaces;

namespace Application.Dto.Input;

public static class EmployeeDto
{
    public record Employee(
        ushort HotelId,
        string FirstName,
        string LastName,
        string? Patronymic,
        DateTimeOffset HireDate
    );

    public record Create(
        ushort HotelId,
        Guid UserId,
        string FirstName,
        string LastName,
        string? Patronymic,
        DateTimeOffset HireDate,
        IClock Clock
    )
    {
        public Domain.Entity.Employee.Employee GetEmployee()
        {
            Domain.Entity.Employee.Employee newEmployee =  new(
                hotelId: HotelId,
                userId: UserId,
                firstName: FirstName,
                lastName: LastName,
                hireDate: HireDate,
                clock: Clock
            );
            if (Patronymic is not null)
            {
                newEmployee.UpdatePatronymic(Patronymic);
            }
            return newEmployee;
        }
    }

    public record Update(
        Guid Id,
        string? FirstName = null,
        string? LastName = null,
        string? Patronymic = null
    )
    {
        public Domain.Entity.Employee.Employee GetUpdateEmployee(Domain.Entity.Employee.Employee employee)
        {
            if (FirstName is not null)
            {
                employee.UpdateFirstName(FirstName);
            }
            if (LastName is not null)
            {
                employee.UpdateLastName(LastName);
            }
            if (Patronymic is not null)
            {
                employee.UpdatePatronymic(Patronymic);
            }
            return employee;
        }
    }

    public record Delete(
        Guid id
    );
}