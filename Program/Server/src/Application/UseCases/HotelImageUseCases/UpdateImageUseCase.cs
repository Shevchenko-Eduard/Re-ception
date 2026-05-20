using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelImageUseCases;

public class UpdateImageUseCase(
    IHotelImageRepository imageRepository,
    IS3HotelImageRepository s3ImageRepository,
    IUnitOfWork unitOfWork) : IAction<HotelImageDTOs.Inner.Update, HotelImage>
{
    private readonly IHotelImageRepository _imageRepository = imageRepository;
    private readonly IS3HotelImageRepository _s3ImageRepository = s3ImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<HotelImage> Execute(HotelImageDTOs.Inner.Update input)
    {
        HotelImage hotelImage = await _imageRepository.GetByIdAsync(input.Id)
            ?? throw new ApplicationException("Hotel image not found");
        HotelImage updatedHotelImage = input.GetHotelImage(hotelImage);

        await _imageRepository.UpdateAsync(updatedHotelImage);
        await _s3ImageRepository.UploadAsync(input.Stream, updatedHotelImage.ImageKey.ToString());
        await _unitOfWork.SaveChangesAsync();
        
        return updatedHotelImage;
    }
}