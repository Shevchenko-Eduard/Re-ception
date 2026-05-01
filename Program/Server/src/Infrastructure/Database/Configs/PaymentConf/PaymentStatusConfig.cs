using Domain.Entity.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.PaymentConf;

public class PaymentStatusConfig : IEntityTypeConfiguration<PaymentStatus>
{
    public void Configure(EntityTypeBuilder<PaymentStatus> builder)
    {
        #region table
        builder.ToTable("payment_statuses");
        #endregion

        #region pk
        builder.HasKey(e => e.Id)
            .HasName("payment_status_id");
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
        builder.HasData(PaymentStatus.All);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(e => e.Id)
            .IsUnique()
            .HasDatabaseName("uq_payment_statuses_id");

        builder.HasIndex(e => e.Name)
            .IsUnique()
            .HasDatabaseName("uq_payment_statuses_name");
        #endregion
    }
}
