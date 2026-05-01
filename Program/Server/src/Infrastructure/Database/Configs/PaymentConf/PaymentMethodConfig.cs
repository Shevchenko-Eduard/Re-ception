using Domain.Entity.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.PaymentConf;

public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        #region table
        builder.ToTable("payment_methods");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("payment_method_id");
        #endregion

        #region property
        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        #endregion

        #region fk
        #endregion

        #region seed_data
        builder.HasData(PaymentMethod.All);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(e => e.Id)
            .IsUnique()
            .HasDatabaseName("uq_payment_methods_id");

        builder.HasIndex(e => e.Name)
            .IsUnique()
            .HasDatabaseName("uq_payment_methods_name");
        #endregion
    }
}
