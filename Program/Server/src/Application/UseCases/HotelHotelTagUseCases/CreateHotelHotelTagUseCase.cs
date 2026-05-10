using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelHotelTagUseCases;

public class CreateHotelHotelTagUseCase(
    IHotelHotelTagRepository hotelHotelTagRepository,
    IUnitOfWork unitOfWork) : IAction<HotelHotelTagDTOs.Create, HotelHotelTag>
{
    private readonly IHotelHotelTagRepository _hotelHotelTagRepository = hotelHotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<HotelHotelTag> Execute(HotelHotelTagDTOs.Create input)
    {
        HotelHotelTag hotelHotelTag = input.GetHotelTag();
        await _hotelHotelTagRepository.AddAsync(hotelHotelTag);
        await _unitOfWork.SaveChangesAsync();
        return hotelHotelTag;
    }
}