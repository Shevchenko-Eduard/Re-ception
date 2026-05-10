using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelUseCases;

public class UpdateHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IAction<HotelDTOs.Update, Hotel>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Hotel> Execute(HotelDTOs.Update input)
    {
        Hotel hotel = await _hotelRepository.GetByIdAsync(input.Id)
            ?? throw new ArgumentException("Hotel with the specified ID not found");
        Hotel updatedHotel = input.GetHotel(hotel);
        await _hotelRepository.UpdateAsync(updatedHotel);
        await _unitOfWork.SaveChangesAsync();
        return updatedHotel;
    }
}