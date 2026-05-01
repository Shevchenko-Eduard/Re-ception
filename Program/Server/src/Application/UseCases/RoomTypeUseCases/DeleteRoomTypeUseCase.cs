using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTypeUseCases;

public class DeleteRoomTypeUseCase(
    IRoomTypeRepository roomTypeRepository,
    IUnitOfWork unitOfWork) : IAction<RoomTypeDTOs.Delete>
{
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;



    public async Task Execute(RoomTypeDTOs.Delete input)
    {

        await _roomTypeRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
