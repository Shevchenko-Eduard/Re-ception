using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomImageUseCases;

public class CreateRoomImageUseCase(
    IRoomImageRepository roomImageRepository,
    IUnitOfWork unitOfWork,
    IS3RoomImageRepository s3RoomImageRepository) : IAction<RoomImageDTOs.Inner.Create, RoomImage>
{
    private readonly IRoomImageRepository _roomImageRepository = roomImageRepository;
    private readonly IS3RoomImageRepository _s3RoomImageRepository = s3RoomImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<RoomImage> Execute(RoomImageDTOs.Inner.Create input)
    {
        RoomImage image = input.GetRoomImage();

        await _roomImageRepository.AddAsync(image);
        await _s3RoomImageRepository.UploadAsync(input.Stream, image.ImageKey.ToString());
        await _unitOfWork.SaveChangesAsync();

        return image;
    }
}