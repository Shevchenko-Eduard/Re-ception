using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class DeleteHotelTagUseCase(
    IHotelTagRepository hotelTagRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<HotelTagDto.Delete>
{
    private readonly IHotelTagRepository _hotelTagRepository = hotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.HotelTag, PermissionFlag.Self);

    public async Task Execute(HotelTagDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        await _hotelTagRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
