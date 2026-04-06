using Domain.Entity.Room;

namespace Application.Dto.Input;

public static class RoomTypeDto
{
    public record Create(
        string Name,
        string Description,
        decimal BasePricePerDay,
        int MaxCountGuest
    )
    {
        public RoomType GetRoomType() => new(
            name: Name,
            description: Description,
            basePrice: BasePricePerDay,
            maxGuest: MaxCountGuest
        );
    }

    public record Update(
        ushort Id,
        string? Name = null,
        string? Description = null,
        decimal? BasePricePerDay = null,
        int? MaxCountGuest = null
    )
    {
        public RoomType GetUpdateRoomType(RoomType roomType)
        {
            if (Name is not null)
            {
                roomType.UpdateName(Name);
            }
            if (Description is not null)
            {
                roomType.UpdateDescription(Description);
            }
            if (BasePricePerDay is not null)
            {
                roomType.UpdateBasePricePerDay((decimal)BasePricePerDay);
            }
            if (MaxCountGuest is not null)
            {
                roomType.UpdateMaxCountGuest((int)MaxCountGuest);
            }
            
            return roomType;
        }
    }

    public record Delete(
        ushort Id
    );
}
