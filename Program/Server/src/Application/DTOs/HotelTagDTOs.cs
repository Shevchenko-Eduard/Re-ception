using Domain.Entity.Hotel;

namespace Application.DTOs;

public static class HotelTagDTOs
{
    public record Create(
        string Name,
        string Description
    )
    {
        public HotelTag GetTag() => new(
            name: Name,
            description: Description
        );
    }

    public record Update(
        int Id,
        string? Name = null,
        string? Description = null
    )
    {
        public HotelTag GetUpdateHotelTag(HotelTag tag)
        {
            tag.UpdateName(Name ?? tag.Name);
            tag.UpdateDescription(Description ?? tag.Description);
            return tag;
        }
    }

    public record Delete(
        int Id
    );
}
