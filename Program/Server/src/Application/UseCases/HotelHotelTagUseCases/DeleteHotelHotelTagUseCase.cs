using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelHotelTagUseCases;

public class DeleteHotelHotelTagUseCase(
    IHotelHotelTagRepository hotelHotelTagRepository,
    IUnitOfWork unitOfWork) : IAction<HotelHotelTagDTOs.Delete>
{
    private readonly IHotelHotelTagRepository _hotelHotelTagRepository = hotelHotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(HotelHotelTagDTOs.Delete input)
    {
        await _hotelHotelTagRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}