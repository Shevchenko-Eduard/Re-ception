using Domain.Entity.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Payment;

public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        #region table
        builder.ToTable("payment_methods");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("id");
        #endregion

        #region property
        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        #endregion

        #region fk
        builder.HasMany(m => m.Payments)
            .WithOne(p => p.PaymentMethod)
            .HasForeignKey(p => p.MethodId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired();
        #endregion

        #region seed_data
        builder.HasData(PaymentMethod.All);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(e => e.Id)
            .IsUnique()
            .HasDatabaseName("ux_payment_methods_id");

        builder.HasIndex(e => e.Name)
            .IsUnique()
            .HasDatabaseName("ux_payment_methods_name");
        #endregion
    }
}
