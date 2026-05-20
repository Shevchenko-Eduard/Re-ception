using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomImageUseCases;

public class UpdateRoomImageUseCase(
    IRoomImageRepository roomImageRepository,
    IS3RoomImageRepository s3RoomImageRepository,
    IUnitOfWork unitOfWork) : IAction<RoomImageDTOs.Inner.Update, RoomImage>
{
    private readonly IRoomImageRepository _roomImageRepository = roomImageRepository;
    private readonly IS3RoomImageRepository _s3RoomImageRepository = s3RoomImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<RoomImage> Execute(RoomImageDTOs.Inner.Update input)
    {
        RoomImage roomImage = await _roomImageRepository.GetByIdAsync(input.Id)
            ?? throw new ApplicationException("Room image not found");
        RoomImage updatedRoomImage = input.GetRoomImage(roomImage);

        await _roomImageRepository.UpdateAsync(updatedRoomImage);
        await _s3RoomImageRepository.UploadAsync(input.Stream, updatedRoomImage.ImageKey.ToString());
        await _unitOfWork.SaveChangesAsync();

        return updatedRoomImage;
    }
}