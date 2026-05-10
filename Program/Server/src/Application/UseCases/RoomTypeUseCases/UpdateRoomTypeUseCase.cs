using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTypeUseCases;

public class UpdateRoomTypeUseCase(
    IRoomTypeRepository roomTypeRepository,
    IUnitOfWork unitOfWork) : IAction<RoomTypeDTOs.Update, RoomType>
{
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;



    public async Task<RoomType> Execute(RoomTypeDTOs.Update input)
    {
        RoomType roomType = await _roomTypeRepository.GetByIdAsync(input.Id)
            ?? throw new ArgumentException("Room type with the specified ID not found");
        RoomType updatedRoomType = input.GetUpdateRoomType(roomType);
        await _roomTypeRepository.UpdateAsync(updatedRoomType);
        await _unitOfWork.SaveChangesAsync();
        return updatedRoomType;
    }
}
