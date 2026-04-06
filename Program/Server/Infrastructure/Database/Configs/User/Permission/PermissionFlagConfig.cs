using Domain.Entity.User.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User.Permission;

public class PermissionFlagConfig : IEntityTypeConfiguration<PermissionFlag>
{
    public void Configure(EntityTypeBuilder<PermissionFlag> builder)
    {
        // Configuration here
    }
}
