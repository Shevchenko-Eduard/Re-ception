using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTypeUseCases;

public class UpdateRoomTypeUseCase(
    IRoomTypeRepository roomTypeRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<RoomTypeDto.Update>
{
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Update, PermissionEntity.RoomType, PermissionFlag.Self);

    public async Task Execute(RoomTypeDto.Update input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        RoomType roomType = await _roomTypeRepository.GetByIdAsync(input.Id) ?? throw new ArgumentException();
        RoomType updatedRoomType = input.GetUpdateRoomType(roomType);
        await _roomTypeRepository.UpdateAsync(updatedRoomType);
        await _unitOfWork.SaveChangesAsync();
    }
}
