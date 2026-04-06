using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User.Role;

public class RoleConfig : IEntityTypeConfiguration<Domain.Entity.User.Role.Role>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.User.Role.Role> builder)
    {
        // Configuration here
    }
}
