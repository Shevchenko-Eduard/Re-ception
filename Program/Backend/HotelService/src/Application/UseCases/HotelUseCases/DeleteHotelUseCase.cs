using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelUseCases;

public class DeleteHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IAction<HotelDTOs.Delete>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(HotelDTOs.Delete input)
    {
        await _hotelRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}