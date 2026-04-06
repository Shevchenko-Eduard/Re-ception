using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomUseCases;

public class UpdateRoomUseCase(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<RoomDto.Update>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;
    public Permission RequiredPermission => new(PermissionAction.Update, PermissionEntity.Room, PermissionFlag.Self);

    public async Task Execute(RoomDto.Update input)
    {
        if(!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        Room room = await _roomRepository.GetByIdAsync(input.Id) ?? throw new ArgumentException();
        Room newRoom = input.GetUpdateRoom(room);
        await _roomRepository.UpdateAsync(newRoom);
        await _unitOfWork.SaveChangesAsync();
    }
}