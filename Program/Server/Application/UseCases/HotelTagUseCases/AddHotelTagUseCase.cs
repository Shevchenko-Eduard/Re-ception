using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class AddHotelTagUseCase(
    IHotelRepository hotelRepository,
    IHotelTagRepository hotelTagRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<HotelTagManagementDto.AddTag>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IHotelTagRepository _hotelTagRepository = hotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Update, PermissionEntity.Hotel, PermissionFlag.Self);

    public async Task Execute(HotelTagManagementDto.AddTag input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }

        var hotel = await _hotelRepository.GetByIdAsync(input.HotelId) ?? throw new ArgumentException("Hotel not found");
        var hotelTag = await _hotelTagRepository.GetByIdAsync(input.HotelTagId) ?? throw new ArgumentException("HotelTag not found");

        hotel.AddHotelTag(hotelTag);
        await _hotelRepository.UpdateAsync(hotel);
        await _unitOfWork.SaveChangesAsync();
    }
}
