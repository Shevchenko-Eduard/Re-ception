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
        services.AddScoped<IS3Repository, MinioRepository>();
        services.AddScoped<IS3HotelImageRepository, MinioHotelImageRepository>();
        services.AddScoped<IS3RoomImageRepository, MinioRoomImageRepository>();

        services.AddScoped<IHotelRepository, EfHotelRepository>();
        services.AddScoped<IHotelTagRepository, EfHotelTagRepository>();
        services.AddScoped<IHotelImageRepository, EfHotelImageRepository>();
        services.AddScoped<IHotelHotelTagRepository, EfHotelHotelTagRepository>();

        services.AddScoped<IPaymentRepository, EfPaymentRepository>();
        services.AddScoped<IPaymentMethodRepository, EfPaymentMethodRepository>();
        services.AddScoped<IPaymentStatusRepository, EfPaymentStatusRepository>();

        services.AddScoped<IReservationRepository, EfReservationRepository>();
        services.AddScoped<IReservationStatusRepository, EfReservationStatusRepository>();

        services.AddScoped<IRoomRepository, EfRoomRepository>();
        services.AddScoped<IRoomImageRepository, EfRoomImageRepository>();
        services.AddScoped<IRoomStatusRepository, EfRoomStatusRepository>();
        services.AddScoped<IRoomTagRepository, EfRoomTagRepository>();
        services.AddScoped<IRoomRoomTagRepository, EfRoomRoomTagRepository>();
        services.AddScoped<IRoomTypeRepository, EfRoomTypeRepository>();

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}