using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.GuestRepository;

namespace Application.UseCases.GuestUseCases;

public class DeleteGuestUseCase(
    IGuestRepository guestRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<GuestDto.Delete>
{
    private readonly IGuestRepository _guestRepository = guestRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.Guest, PermissionFlag.Self);

    public async Task Execute(GuestDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to delete guests");
        }
        await _guestRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
