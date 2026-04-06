using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTagUseCases;

public class CreateRoomTagUseCase(
    IAuthorization authorization,
    IUnitOfWork unitOfWork,
    IRoomTagRepository roomTagRepository) : IUseCase<RoomTagDto.Create>
{
    private readonly IRoomTagRepository _roomTagRepository = roomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.RoomTag, PermissionFlag.Self);

    public async Task Execute(RoomTagDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        RoomTag roomTag = input.GetRoomTag();
        await _roomTagRepository.AddAsync(roomTag);
        await _unitOfWork.SaveChangesAsync();
    }
}
