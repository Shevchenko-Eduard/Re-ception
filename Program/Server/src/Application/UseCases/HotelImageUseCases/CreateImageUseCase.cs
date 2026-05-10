using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelImageUseCases;

public class CreateImageUseCase(
    IHotelImageRepository hotelImageRepository,
    IUnitOfWork unitOfWork) : IAction<HotelImageDTOs.Create, HotelImage>
{
    private readonly IHotelImageRepository _hotelImageRepository = hotelImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<HotelImage> Execute(HotelImageDTOs.Create input)
    {
        HotelImage image = input.GetImage();
        await _hotelImageRepository.CreateAsync(image, input.Stream);
        await _unitOfWork.SaveChangesAsync();
        return image;
    }
}