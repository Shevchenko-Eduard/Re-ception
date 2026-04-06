using Domain.Entity.User.Role;

namespace Application.Dto.Input;

public static class RoleDto
{
    public record Create(
        string Name,
        string? Description = null
    )
    {
        public Role GetRole() => new(
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
        public Role GetUpdateRole(Role role)
        {
            if (Name is not null)
            {
                role.UpdateName(Name);
            }
            if (Description is not null)
            {
                role.UpdateDescription(Description);
            }
            
            return role;
        }
    }

    public record Delete(
        ushort Id
    );
}
