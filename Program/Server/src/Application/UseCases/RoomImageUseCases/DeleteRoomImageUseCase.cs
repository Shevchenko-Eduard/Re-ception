using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomImageUseCases;

public class DeleteRoomImageUseCase(
    IRoomImageRepository roomImageRepository,
    IS3RoomImageRepository s3RoomImageRepository,
    IUnitOfWork unitOfWork) : IAction<RoomImageDTOs.Request.Delete>
{
    private readonly IRoomImageRepository _roomImageRepository = roomImageRepository;
    private readonly IS3RoomImageRepository _s3RoomImageRepository = s3RoomImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(RoomImageDTOs.Request.Delete input)
    {
        RoomImage roomImage = await _roomImageRepository.GetByIdAsync(input.Id)
            ?? throw new ApplicationException("Room image not found");

        await _s3RoomImageRepository.DeleteAsync(roomImage.ImageKey.ToString());
        await _roomImageRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}