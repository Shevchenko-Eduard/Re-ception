using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Entity.User.Permission;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelUseCases;

public class UpdateHotelUseCase(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<HotelDto.Update>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Update, PermissionEntity.Hotel, PermissionFlag.Self);

    public async Task Execute(HotelDto.Update input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to update hotels");
        }
        Hotel hotel = await _hotelRepository.GetByIdAsync(input.Id)
            ?? throw new ArgumentException("Hotel with the specified ID not found");
        if (hotel.Id != input.Id)
        {
            throw new ArgumentException("Hotel ID mismatch");
        }
        Hotel newHotel = input.GetHotel(hotel);
        await _hotelRepository.UpdateAsync(newHotel);
        await _unitOfWork.SaveChangesAsync();
    }
}