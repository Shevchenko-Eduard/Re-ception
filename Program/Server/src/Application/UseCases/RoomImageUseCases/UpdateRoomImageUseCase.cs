using Application.DTOs;
using Application.Interfaces;
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
        await _roomImageRepository.UpdateAsync(input.Id, input.Stream);
        await _unitOfWork.SaveChangesAsync();
    }
}