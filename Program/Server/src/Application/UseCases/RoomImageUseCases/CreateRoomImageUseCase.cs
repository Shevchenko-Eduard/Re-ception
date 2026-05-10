using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomImageUseCases;

public class CreateRoomImageUseCase(
    IRoomImageRepository roomImageRepository,
    IUnitOfWork unitOfWork) : IAction<RoomImageDTOs.Create, RoomImage>
{
    private readonly IRoomImageRepository _roomImageRepository = roomImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<RoomImage> Execute(RoomImageDTOs.Create input)
    {
        RoomImage image = input.GetImage();
        await _roomImageRepository.CreateAsync(image, input.Stream);
        await _unitOfWork.SaveChangesAsync();
        return image;
    }
}