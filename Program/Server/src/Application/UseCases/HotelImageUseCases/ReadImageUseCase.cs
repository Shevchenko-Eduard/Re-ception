using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelImageUseCases;

public class ReadImageUseCase(
    IHotelImageRepository imageRepository,
    IS3HotelImageRepository s3ImageRepository) : IQuestion<HotelImageDTOs.Response.Read, HotelImageDTOs.Request.Read>
{
    private readonly IHotelImageRepository _imageRepository = imageRepository;
    private readonly IS3HotelImageRepository _s3ImageRepository = s3ImageRepository;
    public async Task<HotelImageDTOs.Response.Read> Ask(HotelImageDTOs.Request.Read input)
    {
        HotelImage hotelImage = await _imageRepository.GetByIdAsync(input.Id)
            ?? throw new ApplicationException("Hotel image not found");

        var stream = await _s3ImageRepository.DownloadAsync(hotelImage.ImageKey.ToString());

        return new HotelImageDTOs.Response.Read(
            Stream: stream,
            ContentType: hotelImage.ContentType,
            FileName: $"{hotelImage.Id}.{hotelImage.Extension}"
        );
    }
}