using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class CreateHotelTagUseCase(
    IHotelTagRepository hotelTagRepository,
    IUnitOfWork unitOfWork) : IAction<HotelTagDTOs.Create>
{
    private readonly IHotelTagRepository _hotelTagRepository = hotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(HotelTagDTOs.Create input)
    {
        await _hotelTagRepository.AddAsync(input.GetHotelTag());
        await _unitOfWork.SaveChangesAsync();
    }
}