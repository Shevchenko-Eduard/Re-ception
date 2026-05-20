using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelImageUseCases;

public class DeleteImageUseCase(
    IHotelImageRepository imageRepository,
    IUnitOfWork unitOfWork,
    IS3HotelImageRepository s3ImageRepository) : IAction<HotelImageDTOs.Request.Delete>
{
    private readonly IHotelImageRepository _imageRepository = imageRepository;
    private readonly IS3HotelImageRepository _s3ImageRepository = s3ImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(HotelImageDTOs.Request.Delete input)
    {
        var image = await _imageRepository.GetByIdAsync(input.Id)
            ?? throw new System.Exception("Image not found");

        await _s3ImageRepository.DeleteAsync(image.ImageKey.ToString());
        await _imageRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}