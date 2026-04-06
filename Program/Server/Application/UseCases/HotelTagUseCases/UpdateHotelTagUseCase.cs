using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class UpdateHotelTagUseCase(
    IHotelTagRepository hotelTagRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<HotelTagDto.Update>
{
    private readonly IHotelTagRepository _hotelTagRepository = hotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Update, PermissionEntity.HotelTag, PermissionFlag.Self);

    public async Task Execute(HotelTagDto.Update input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        HotelTag hotelTag = await _hotelTagRepository.GetByIdAsync(input.Id) ?? throw new ArgumentException();
        HotelTag updatedHotelTag = input.GetUpdateHotelTag(hotelTag);
        await _hotelTagRepository.UpdateAsync(updatedHotelTag);
        await _unitOfWork.SaveChangesAsync();
    }
}
