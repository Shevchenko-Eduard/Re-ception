using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTypeUseCases;

public class CreateRoomTypeUseCase(
    IAuthorization authorization,
    IUnitOfWork unitOfWork,
    IRoomTypeRepository roomTypeRepository) : IUseCase<RoomTypeDto.Create>
{
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.RoomType, PermissionFlag.Self);

    public async Task Execute(RoomTypeDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        RoomType roomType = input.GetRoomType();
        await _roomTypeRepository.AddAsync(roomType);
        await _unitOfWork.SaveChangesAsync();
    }
}
