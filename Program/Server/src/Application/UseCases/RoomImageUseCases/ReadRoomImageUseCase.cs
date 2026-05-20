using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomImageUseCases;

public class ReadRoomImageUseCase(
    IRoomImageRepository imageRepository,
    IS3RoomImageRepository s3ImageRepository) : IQuestion<RoomImageDTOs.Response.Read, RoomImageDTOs.Request.Read>
{
    private readonly IRoomImageRepository _imageRepository = imageRepository;
    private readonly IS3RoomImageRepository _s3ImageRepository = s3ImageRepository;
    public async Task<RoomImageDTOs.Response.Read> Ask(RoomImageDTOs.Request.Read input)
    {
        RoomImage roomImage = await _imageRepository.GetByIdAsync(input.Id)
            ?? throw new ApplicationException("Room image not found");

        var stream = await _s3ImageRepository.DownloadAsync(roomImage.ImageKey.ToString());

        return new RoomImageDTOs.Response.Read(
            Stream: stream,
            ContentType: roomImage.ContentType,
            FileName: $"{roomImage.Id}.{roomImage.Extension}"
        );
    }
}