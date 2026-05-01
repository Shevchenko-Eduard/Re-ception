using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class CreateHotelTagUseCase(
    IUnitOfWork unitOfWork,
    IHotelTagRepository hotelTagRepository) : IAction<HotelTagDTOs.Create>
{
    private readonly IHotelTagRepository _hotelTagRepository = hotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(HotelTagDTOs.Create input)
    {
        HotelTag tag = input.GetTag();
        await _hotelTagRepository.AddAsync(tag);
        await _unitOfWork.SaveChangesAsync();
    }
}
