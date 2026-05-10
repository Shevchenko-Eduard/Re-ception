using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTypeUseCases;

public class CreateRoomTypeUseCase(

    IUnitOfWork unitOfWork,
    IRoomTypeRepository roomTypeRepository) : IAction<RoomTypeDTOs.Create, RoomType>
{
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;



    public async Task<RoomType> Execute(RoomTypeDTOs.Create input)
    {

        RoomType roomType = input.GetRoomType();
        await _roomTypeRepository.AddAsync(roomType);
        await _unitOfWork.SaveChangesAsync();
        return roomType;
    }
}
