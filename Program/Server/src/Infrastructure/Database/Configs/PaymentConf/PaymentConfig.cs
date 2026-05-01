using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs.PaymentConf;

public class PaymentConfig : IEntityTypeConfiguration<Domain.Entity.Payment.Payment>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Payment.Payment> builder)
    {
        #region table
        builder.ToTable("payments");
        #endregion

        #region pk
        builder.HasKey(p => p.Id)
            .HasName("payment_id");
        #endregion

        #region property
        builder.Property(p => p.Amount)
            .HasColumnName("amount")
            .HasPrecision(19, 4)
            .IsRequired();

        builder.Property(p => p.PaymentDate)
            .HasColumnName("payment_date")
            .IsRequired();

        builder.Property(p => p.MethodId)
            .HasColumnName("method_id")
            .IsRequired();

        builder.Property(p => p.StatusId)
            .HasColumnName("status_id")
            .IsRequired();

        builder.Property(p => p.ReservationId)
            .HasColumnName("reservation_id");
        #endregion

        #region fk
        builder.HasOne(p => p.PaymentMethod)
            .WithMany(m => m.Payments)
            .HasForeignKey(p => p.MethodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.PaymentStatus)
            .WithMany(s => s.Payments)
            .HasForeignKey(p => p.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Reservation)
            .WithMany(r => r.Payments)
            .HasForeignKey(p => p.ReservationId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
        #endregion

        #region ignore
        #endregion

        #region index
        // Поиск платежей по бронированию (самый частый запрос)
        builder.HasIndex(p => p.ReservationId)
            .HasDatabaseName("idx_payments_reservation_id");

        // Фильтрация по статусу (например, "показать все незавершенные платежи")
        builder.HasIndex(p => p.StatusId)
            .HasDatabaseName("idx_payments_status_id");

        // Отчеты по датам + статусам
        builder.HasIndex(p => new { p.PaymentDate, p.StatusId })
            .HasDatabaseName("idx_payments_date_status");

        // Поиск по методу оплаты (аналитика)
        builder.HasIndex(p => p.MethodId)
            .HasDatabaseName("idx_payments_method_id");
        #endregion
    }
}
