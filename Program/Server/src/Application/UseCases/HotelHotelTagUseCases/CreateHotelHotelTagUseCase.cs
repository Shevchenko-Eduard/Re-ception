using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelHotelTagUseCases;

public class CreateHotelHotelTagUseCase(
    IHotelHotelTagRepository hotelHotelTagRepository,
    IUnitOfWork unitOfWork) : IAction<HotelHotelTagDTOs.Create>
{
    private readonly IHotelHotelTagRepository _hotelHotelTagRepository = hotelHotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(HotelHotelTagDTOs.Create input)
    {
        await _hotelHotelTagRepository.AddAsync(input.GetHotelTag());
        await _unitOfWork.SaveChangesAsync();
    }
}