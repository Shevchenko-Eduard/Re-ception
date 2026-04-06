using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelUseCases;

public class DeleteHotelUseCase(
    IAuthorization authorization,
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : IUseCase<HotelDto.Delete>
{
    private readonly IAuthorization _authorization = authorization;
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.Hotel, PermissionFlag.Self);

    public async Task Execute(HotelDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        await _hotelRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}