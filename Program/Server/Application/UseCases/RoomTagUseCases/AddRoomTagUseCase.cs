using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTagUseCases;

public class AddRoomTagUseCase(
    IRoomRepository roomRepository,
    IRoomTagRepository roomTagRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<RoomTagManagementDto.AddTag>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IRoomTagRepository _roomTagRepository = roomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Update, PermissionEntity.Room, PermissionFlag.Self);

    public async Task Execute(RoomTagManagementDto.AddTag input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }

        var room = await _roomRepository.GetByIdAsync(input.RoomId) ?? throw new ArgumentException("Room not found");
        var roomTag = await _roomTagRepository.GetByIdAsync(input.RoomTagId) ?? throw new ArgumentException("RoomTag not found");

        room.AddRoomTag(roomTag);
        await _roomRepository.UpdateAsync(room);
        await _unitOfWork.SaveChangesAsync();
    }
}
