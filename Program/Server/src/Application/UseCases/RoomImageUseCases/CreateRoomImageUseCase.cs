using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomImageUseCases;

public class CreateRoomImageUseCase(
    IRoomImageRepository roomImageRepository,
    IUnitOfWork unitOfWork) : IAction<RoomImageDTOs.Create>
{
    private readonly IRoomImageRepository _roomImageRepository = roomImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(RoomImageDTOs.Create input)
    {
        await _roomImageRepository.CreateAsync(input.GetImage(), input.Stream);
        await _unitOfWork.SaveChangesAsync();
    }
}