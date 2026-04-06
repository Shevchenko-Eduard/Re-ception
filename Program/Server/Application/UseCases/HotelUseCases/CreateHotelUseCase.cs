using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelUseCases;

public class CreateHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<HotelDto.Create>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;
    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.Hotel, PermissionFlag.Any);
    public async Task Execute(HotelDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        await _hotelRepository.AddAsync(input.GetHotel());
        await _unitOfWork.SaveChangesAsync();
    }
}
