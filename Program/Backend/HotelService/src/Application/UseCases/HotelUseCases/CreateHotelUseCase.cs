using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelUseCases;

public class CreateHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IAction<HotelDTOs.Create>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(HotelDTOs.Create input)
    {
        await _hotelRepository.AddAsync(input.GetHotel());
        await _unitOfWork.SaveChangesAsync();
    }
}
