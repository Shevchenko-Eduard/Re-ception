using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Payment;

public class PaymentConfig : IEntityTypeConfiguration<Domain.Entity.Payment.Payment>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Payment.Payment> builder)
    {
        // Configuration here
    }
}
