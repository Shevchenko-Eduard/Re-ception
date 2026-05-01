using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelImageUseCases;

public class CreateImageUseCase(
    IHotelImageRepository hotelImageRepository,
    IUnitOfWork unitOfWork) : IAction<HotelImageDTOs.Create>
{
    private readonly IHotelImageRepository _hotelImageRepository = hotelImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(HotelImageDTOs.Create input)
    {
        await _hotelImageRepository.CreateAsync(input.GetImage(), input.Stream);
        await _unitOfWork.SaveChangesAsync();
    }
}