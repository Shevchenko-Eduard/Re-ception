using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User;

public class UserConfig : IEntityTypeConfiguration<Domain.Entity.User.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.User.User> builder)
    {
        // Configuration here
    }
}
