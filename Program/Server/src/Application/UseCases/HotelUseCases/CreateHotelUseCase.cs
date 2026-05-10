using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelUseCases;

public class CreateHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IAction<HotelDTOs.Create, Hotel>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Hotel> Execute(HotelDTOs.Create input)
    {
        var hotel = input.GetHotel();
        await _hotelRepository.AddAsync(hotel);
        await _unitOfWork.SaveChangesAsync();
        return hotel;
    }
}
