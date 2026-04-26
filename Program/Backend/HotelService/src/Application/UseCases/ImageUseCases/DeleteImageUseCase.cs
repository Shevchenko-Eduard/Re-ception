using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.HotelRepository;

namespace Application.UseCases.ImageUseCases;

public class DeleteImageUseCase(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork) : IAction<ImageDTOs.Delete>
{
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(ImageDTOs.Delete input)
    {
        await _imageRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}