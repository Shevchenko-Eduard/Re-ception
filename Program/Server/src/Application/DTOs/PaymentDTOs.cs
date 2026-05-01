using Domain.Entity.Payment;

namespace Application.DTOs;

public static class PaymentDTOs
{
    public record Create(
        int ReservationId,
        int StatusId,
        int MethodId,
        decimal Amount,
        DateTimeOffset PaymentDate
    )
    {
        public Payment GetPayment() => new(
            reservationId: ReservationId,
            statusId: StatusId,
            methodId: MethodId,
            amount: Amount,
            paymentDate: PaymentDate
        );
    }

    public record Delete(
        int Id
    );
}
