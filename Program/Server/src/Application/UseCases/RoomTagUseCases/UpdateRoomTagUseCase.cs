using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTagUseCases;

public class UpdateRoomTagUseCase(
    IRoomTagRepository roomTagRepository,
    IUnitOfWork unitOfWork) : IAction<RoomTagDTOs.Update, RoomTag>
{
    private readonly IRoomTagRepository _roomTagRepository = roomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;



    public async Task<RoomTag> Execute(RoomTagDTOs.Update input)
    {

        RoomTag roomTag = await _roomTagRepository.GetByIdAsync(input.Id) ?? throw new ArgumentException();
        RoomTag updatedRoomTag = input.GetUpdateRoomTag(roomTag);
        await _roomTagRepository.UpdateAsync(updatedRoomTag);
        await _unitOfWork.SaveChangesAsync();
        return updatedRoomTag;
    }
}
