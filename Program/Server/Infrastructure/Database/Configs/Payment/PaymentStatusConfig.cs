using Domain.Entity.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.Payment;

public class PaymentStatusConfig : IEntityTypeConfiguration<PaymentStatus>
{
    public void Configure(EntityTypeBuilder<PaymentStatus> builder)
    {
        #region table
        builder.ToTable("payment_statuses");
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
            .WithOne(p => p.PaymentStatus)
            .HasForeignKey(p => p.StatusId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired();
        #endregion

        #region seed_data
        builder.HasData(PaymentStatus.All);
        #endregion

        #region ignore
        #endregion

        #region index
        builder.HasIndex(e => e.Id)
            .IsUnique()
            .HasDatabaseName("ux_payment_statuses_id");

        builder.HasIndex(e => e.Name)
            .IsUnique()
            .HasDatabaseName("ux_payment_statuses_name");
        #endregion
    }
}
