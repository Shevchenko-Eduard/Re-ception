using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomUseCases;

public class DeleteRoomUseCase(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<RoomDto.Delete>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;
    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.Room, PermissionFlag.Self);

    public async Task Execute(RoomDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        await _roomRepository.DeleteAsync(input.id);
        await _unitOfWork.SaveChangesAsync();
    }
}