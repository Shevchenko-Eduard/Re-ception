using Application.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;
using Domain.Interfaces.Repositories.PaymentRepository;
using Domain.Interfaces.Repositories.ReservationRepository;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.EfRepository;
using Infrastructure.EfRepository.HotelRepository;
using Infrastructure.EfRepository.PaymentRepository;
using Infrastructure.EfRepository.ReservationRepository;
using Infrastructure.EfRepository.RoomRepository;
using Infrastructure.Interfaces.Repository;
using Infrastructure.MinioRepository;
using Microsoft.Extensions.DependencyInjection;

namespace LibWeb.DependencyInjection;

public partial class DependencyInjectionConfig
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IS3Repository, MinioRepository>();
        services.AddTransient<IS3HotelImageRepository, MinioHotelImageRepository>();
        services.AddTransient<IS3RoomImageRepository, MinioRoomImageRepository>();

        services.AddTransient<IHotelRepository, EfHotelRepository>();
        services.AddTransient<IHotelTagRepository, EfHotelTagRepository>();
        services.AddTransient<IHotelImageRepository, EfHotelImageRepository>();
        services.AddTransient<IHotelHotelTagRepository, EfHotelHotelTagRepository>();

        services.AddTransient<IPaymentRepository, EfPaymentRepository>();
        services.AddTransient<IPaymentMethodRepository, EfPaymentMethodRepository>();
        services.AddTransient<IPaymentStatusRepository, EfPaymentStatusRepository>();

        services.AddTransient<IReservationRepository, EfReservationRepository>();
        services.AddTransient<IReservationStatusRepository, EfReservationStatusRepository>();

        services.AddTransient<IRoomRepository, EfRoomRepository>();
        services.AddTransient<IRoomImageRepository, EfRoomImageRepository>();
        services.AddTransient<IRoomStatusRepository, EfRoomStatusRepository>();
        services.AddTransient<IRoomTagRepository, EfRoomTagRepository>();
        services.AddTransient<IRoomRoomTagRepository, EfRoomRoomTagRepository>();
        services.AddTransient<IRoomTypeRepository, EfRoomTypeRepository>();

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}