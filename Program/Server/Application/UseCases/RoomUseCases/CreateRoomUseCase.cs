using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomUseCases;

public class CreateRoomUseCase(
    IAuthorization authorization,
    IUnitOfWork unitOfWork,
    IRoomRepository roomRepository) : IUseCase<RoomDto.Create>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.Room, PermissionFlag.Self);

    public async Task Execute(RoomDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        Room room = input.GetRoom();
        await _roomRepository.AddAsync(room);
        await _unitOfWork.SaveChangesAsync();
    }
}