using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTagUseCases;

public class CreateRoomTagUseCase(

    IUnitOfWork unitOfWork,
    IRoomTagRepository roomTagRepository) : IAction<RoomTagDTOs.Create>
{
    private readonly IRoomTagRepository _roomTagRepository = roomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(RoomTagDTOs.Create input)
    {

        RoomTag roomTag = input.GetRoomTag();
        await _roomTagRepository.AddAsync(roomTag);
        await _unitOfWork.SaveChangesAsync();
    }
}
