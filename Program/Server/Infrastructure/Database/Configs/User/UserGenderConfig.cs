using Domain.Entity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.User;

public class UserGenderConfig : IEntityTypeConfiguration<UserGender>
{
    public void Configure(EntityTypeBuilder<UserGender> builder)
    {
        // Configuration here
    }
}
