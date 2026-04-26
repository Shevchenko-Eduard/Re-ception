using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.HotelRepository;

namespace Application.UseCases.ImageUseCases;

public class UpdateImageUseCase(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork) : IAction<ImageDTOs.Update>
{
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(ImageDTOs.Update input)
    {
        Image image = await _imageRepository.GetByIdAsync(input.Id)
            ?? throw new Exception();
        Image updatedImage = input.GetImage(image);
        await _imageRepository.UpdateAsync(updatedImage);
        await _unitOfWork.SaveChangesAsync();
    }
}