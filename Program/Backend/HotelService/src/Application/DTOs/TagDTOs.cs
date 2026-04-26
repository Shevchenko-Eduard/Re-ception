using Domain.Entity.Hotel;

namespace Application.DTOs;

public static class TagDTOs
{
    public record Create(
        string Name,
        string Description
    )
    {
        public Tag GetTag() => new(
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
        public Tag GetUpdateHotelTag(Tag tag)
        {
            if (Name is not null)
            {
                tag.UpdateName(Name);
            }
            if (Description is not null)
            {
                tag.UpdateDescription(Description);
            }
            
            return tag;
        }
    }

    public record Delete(
        ushort Id
    );
}
