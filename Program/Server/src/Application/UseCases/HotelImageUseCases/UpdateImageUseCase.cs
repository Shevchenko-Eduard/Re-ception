using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelImageUseCases;

public class UpdateImageUseCase(
    IHotelImageRepository imageRepository,
    IUnitOfWork unitOfWork) : IAction<HotelImageDTOs.Update>
{
    private readonly IHotelImageRepository _imageRepository = imageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(HotelImageDTOs.Update input)
    {
        HotelImage image = await _imageRepository.GetValueByIdAsync(input.Id)
            ?? throw new Exception();
        await _imageRepository.UpdateAsync(image, input.Stream);
        await _unitOfWork.SaveChangesAsync();
    }
}