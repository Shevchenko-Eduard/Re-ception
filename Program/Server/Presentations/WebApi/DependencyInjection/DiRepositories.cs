using Application.Interfaces;
using Domain.Interfaces.Repositories.EmployeeRepository;
using Domain.Interfaces.Repositories.GuestRepository;
using Domain.Interfaces.Repositories.HotelRepository;
using Domain.Interfaces.Repositories.PaymentRepository;
using Domain.Interfaces.Repositories.ReservationRepository;
using Domain.Interfaces.Repositories.RoomRepository;
using Domain.Interfaces.Repositories.UserRepository;
using Domain.Interfaces.Repositories.UserRepository.Permission;
using Domain.Interfaces.Repositories.UserRepository.Role;
using Infrastructure.EfRepository;
using Infrastructure.EfRepository.EmployeeRepository;
using Infrastructure.EfRepository.GuestRepository;
using Infrastructure.EfRepository.HotelRepository;
using Infrastructure.EfRepository.PaymentRepository;
using Infrastructure.EfRepository.ReservationRepository;
using Infrastructure.EfRepository.RoomRepository;
using Infrastructure.EfRepository.UserRepository;
using Infrastructure.EfRepository.UserRepository.Permission;
using Infrastructure.EfRepository.UserRepository.Role;

namespace WebApi.DependencyInjection;

public partial class DependencyInjectionConfig
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EfEmployeeRepository>();

        services.AddScoped<IGuestRepository, EfGuestRepository>();

        services.AddScoped<IHotelRepository, EfHotelRepository>();
        services.AddScoped<IHotelTagRepository, EfHotelTagRepository>();

        services.AddScoped<IPaymentRepository, EfPaymentRepository>();
        services.AddScoped<IPaymentMethodRepository, EfPaymentMethodRepository>();
        services.AddScoped<IPaymentStatusRepository, EfPaymentStatusRepository>();

        services.AddScoped<IReservationRepository, EfReservationRepository>();
        services.AddScoped<IReservationStatusRepository, EfReservationStatusRepository>();

        services.AddScoped<IRoomRepository, EfRoomRepository>();
        services.AddScoped<IRoomStatusRepository, EfRoomStatusRepository>();
        services.AddScoped<IRoomTagRepository, EfRoomTagRepository>();
        services.AddScoped<IRoomTypeRepository, EfRoomTypeRepository>();

        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddScoped<IUserGenderRepository, EfUserGenderRepository>();
        services.AddScoped<IUserPermissionRepository, EfUserPermissionRepository>();
        services.AddScoped<IUserRoleRepository, EfUserRoleRepository>();

        services.AddScoped<IPermissionRepository, EfPermissionRepository>();
        services.AddScoped<IPermissionEntityRepository, EfPermissionEntityRepository>();
        services.AddScoped<IPermissionFlagRepository, EfPermissionFlagRepository>();
        services.AddScoped<IPermissionActionRepository, EfPermissionActionRepository>();

        services.AddScoped<IRoleRepository, EfRoleRepository>();

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}