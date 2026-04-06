using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class CreateHotelTagUseCase(
    IAuthorization authorization,
    IUnitOfWork unitOfWork,
    IHotelTagRepository hotelTagRepository) : IUseCase<HotelTagDto.Create>
{
    private readonly IHotelTagRepository _hotelTagRepository = hotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.HotelTag, PermissionFlag.Self);

    public async Task Execute(HotelTagDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        HotelTag hotelTag = input.GetHotelTag();
        await _hotelTagRepository.AddAsync(hotelTag);
        await _unitOfWork.SaveChangesAsync();
    }
}
