using Domain.Entity.Payment;

namespace Application.Dto.Input;

public static class PaymentDto
{
    public record Create(
        ulong ReservationId,
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
        uint Id
    );
}
