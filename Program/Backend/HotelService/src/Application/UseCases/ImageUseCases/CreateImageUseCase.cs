using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.HotelRepository;

namespace Application.UseCases.ImageUseCases;

public class CreateImageUseCase(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork) : IAction<ImageDTOs.Create>
{
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(ImageDTOs.Create input)
    {
        await _imageRepository.AddAsync(input.GetImage());
        await _unitOfWork.SaveChangesAsync();
    }
}