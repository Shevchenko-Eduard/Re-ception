using Domain.Interfaces;

namespace Application.Dto.Input;

public static class GuestDto
{
    public record Guest();

    public record Create(
        Guid UserId,
        IClock Clock
    )
    {
        public Domain.Entity.Guest.Guest GetGuest()
        {
            return new Domain.Entity.Guest.Guest(
                userId: UserId,
                clock: Clock
            );
        }
    }

    public record Delete(
        Guid Id
    );
}