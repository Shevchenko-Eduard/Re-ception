using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Employee;

public class EmployeeConfig : IEntityTypeConfiguration<Domain.Entity.Employee.Employee>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Employee.Employee> builder)
    {
        // Configuration here
    }
}
