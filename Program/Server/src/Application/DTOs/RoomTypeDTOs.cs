using Domain.Entity.Room;

namespace Application.DTOs;

public static class RoomTypeDTOs
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
        int Id,
        string? Name = null,
        string? Description = null,
        decimal? BasePricePerDay = null,
        int? MaxCountGuest = null
    )
    {
        public RoomType GetUpdateRoomType(RoomType roomType)
        {
            roomType.UpdateName(Name ?? roomType.Name);
            roomType.UpdateDescription(Description ?? roomType.Description);
            roomType.UpdateBasePricePerDay(BasePricePerDay ?? roomType.BasePricePerDay);
            roomType.UpdateMaxCountGuest(MaxCountGuest ?? roomType.MaxCountGuest);
            return roomType;
        }
    }

    public record Delete(
        int Id
    );
}
