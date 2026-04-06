using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User.Permission;

public class PermissionConfig : IEntityTypeConfiguration<Domain.Entity.User.Permission.Permission>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.User.Permission.Permission> builder)
    {
        // Configuration here
    }
}
