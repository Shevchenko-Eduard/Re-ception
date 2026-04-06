using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database.IdentityEntity;

public class ApplicationUser : IdentityUser
{
    public Guid AppUserId { get; set; }
}