using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class DeleteHotelTagUseCase(
    IHotelTagRepository hotelTagRepository,
    IUnitOfWork unitOfWork) : IAction<HotelTagDTOs.Delete>
{
    private readonly IHotelTagRepository _hotelTagRepository = hotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(HotelTagDTOs.Delete input)
    {
        await _hotelTagRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
