using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelImageUseCases;

public class DeleteImageUseCase(
    IHotelImageRepository imageRepository,
    IUnitOfWork unitOfWork) : IAction<HotelImageDTOs.Delete>
{
    private readonly IHotelImageRepository _imageRepository = imageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(HotelImageDTOs.Delete input)
    {
        await _imageRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}