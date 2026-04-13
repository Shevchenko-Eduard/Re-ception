using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQLitePCL;

namespace Infrastructure.Database.Configs.Employee;

public class EmployeeConfig : IEntityTypeConfiguration<Domain.Entity.Employee.Employee>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Employee.Employee> builder)
    {
        #region table
        builder.ToTable("employee");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(e => e.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Patronymic)
            .HasColumnName("patronymic")
            .HasMaxLength(50);

        builder.Property(e => e.CreateAt)
            .HasColumnName("create_at")
            .IsRequired();

        builder.Property(e => e.HireDate)
            .HasColumnName("hire_date")
            .IsRequired();

        builder.Property(e => e.HotelId)
            .HasColumnName("hotel_id")
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        #endregion

        #region fk
        builder.HasOne(e => e.User)
            .WithOne(u => u.Employee)
            .HasForeignKey<Domain.Entity.Employee.Employee>(e => e.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Hotel)
            .WithMany(h => h.Employees)
            .HasForeignKey(e => e.HotelId)
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(e => new { e.FirstName, e.LastName, e.Patronymic })
            .HasDatabaseName("ix_employee_full_name");
        builder.HasIndex(e => e.HotelId)
            .HasDatabaseName("ix_employee_hotel_id");
        #endregion
    }
}
