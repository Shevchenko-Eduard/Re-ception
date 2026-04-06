using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTypeUseCases;

public class DeleteRoomTypeUseCase(
    IRoomTypeRepository roomTypeRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<RoomTypeDto.Delete>
{
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.RoomType, PermissionFlag.Self);

    public async Task Execute(RoomTypeDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        await _roomTypeRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
