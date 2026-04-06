using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTagUseCases;

public class DeleteRoomTagUseCase(
    IRoomTagRepository roomTagRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<RoomTagDto.Delete>
{
    private readonly IRoomTagRepository _roomTagRepository = roomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.RoomTag, PermissionFlag.Self);

    public async Task Execute(RoomTagDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        await _roomTagRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
