using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelImageUseCases;

public class CreateImageUseCase(
    IHotelImageRepository hotelImageRepository,
    IS3HotelImageRepository s3ImageRepository,
    IUnitOfWork unitOfWork) : IAction<HotelImageDTOs.Inner.Create, HotelImage>
{
    private readonly IHotelImageRepository _hotelImageRepository = hotelImageRepository;
    private readonly IS3HotelImageRepository _s3ImageRepository = s3ImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<HotelImage> Execute(HotelImageDTOs.Inner.Create input)
    {
        HotelImage image = input.GetHotelImage();

        await _hotelImageRepository.AddAsync(image);
        await _s3ImageRepository.UploadAsync(input.Stream, image.ImageKey.ToString());
        await _unitOfWork.SaveChangesAsync();

        return image;
    }
}