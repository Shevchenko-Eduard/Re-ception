using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.GuestRepository;

namespace Application.UseCases.GuestUseCases;

public class CreateGuestUseCase(
    IAuthorization authorization,
    IUnitOfWork unitOfWork,
    IGuestRepository guestRepository) : IUseCase<GuestDto.Create>
{
    private readonly IGuestRepository _guestRepository = guestRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.Guest, PermissionFlag.Self);

    public async Task Execute(GuestDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to create guests");
        }
        Domain.Entity.Guest.Guest guest = input.GetGuest();
        await _guestRepository.AddAsync(guest);
        await _unitOfWork.SaveChangesAsync();
    }
}
