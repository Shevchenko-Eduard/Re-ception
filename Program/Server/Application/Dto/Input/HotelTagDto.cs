using Domain.Entity.Hotel;

namespace Application.Dto.Input;

public static class HotelTagDto
{
    public record Create(
        string Name,
        string Description
    )
    {
        public HotelTag GetHotelTag() => new(
            name: Name,
            description: Description
        );
    }

    public record Update(
        ushort Id,
        string? Name = null,
        string? Description = null
    )
    {
        public HotelTag GetUpdateHotelTag(HotelTag hotelTag)
        {
            if (Name is not null)
            {
                hotelTag.UpdateName(Name);
            }
            if (Description is not null)
            {
                hotelTag.UpdateDescription(Description);
            }
            
            return hotelTag;
        }
    }

    public record Delete(
        ushort Id
    );
}
