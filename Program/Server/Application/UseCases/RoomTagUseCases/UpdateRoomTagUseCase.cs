using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTagUseCases;

public class UpdateRoomTagUseCase(
    IRoomTagRepository roomTagRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<RoomTagDto.Update>
{
    private readonly IRoomTagRepository _roomTagRepository = roomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Update, PermissionEntity.RoomTag, PermissionFlag.Self);

    public async Task Execute(RoomTagDto.Update input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        RoomTag roomTag = await _roomTagRepository.GetByIdAsync(input.Id) ?? throw new ArgumentException();
        RoomTag updatedRoomTag = input.GetUpdateRoomTag(roomTag);
        await _roomTagRepository.UpdateAsync(updatedRoomTag);
        await _unitOfWork.SaveChangesAsync();
    }
}
