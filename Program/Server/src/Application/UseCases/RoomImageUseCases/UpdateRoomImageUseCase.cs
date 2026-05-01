using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomImageUseCases;

public class UpdateRoomImageUseCase(
    IRoomImageRepository roomImageRepository,
    IUnitOfWork unitOfWork) : IAction<RoomImageDTOs.Update>
{
    private readonly IRoomImageRepository _roomImageRepository = roomImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(RoomImageDTOs.Update input)
    {
        RoomImage image = await _roomImageRepository.GetValueByIdAsync(input.Id)
            ?? throw new Exception();
        await _roomImageRepository.UpdateAsync(image, input.Stream);
        await _unitOfWork.SaveChangesAsync();
    }
}