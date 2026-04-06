using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelUseCases;

public class GetHotelUseCase(
    IAuthorization authorization,
    IHotelRepository hotelRepository) : IUseCase<object, IEnumerable<HotelDto.GetAll>>
{
    private readonly IAuthorization _authorization = authorization;
    private readonly IHotelRepository _hotelRepository = hotelRepository;

    public Permission RequiredPermission => new(PermissionAction.Read, PermissionEntity.Hotel, PermissionFlag.Self);

    public async Task<IEnumerable<HotelDto.GetAll>> Execute(object input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        var hotels = await _hotelRepository.GetAllAsync();
        return HotelDto.GetAll.FromListHotels(hotels);
    }
}