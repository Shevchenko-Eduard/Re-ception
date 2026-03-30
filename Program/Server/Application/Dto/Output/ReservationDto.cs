using System.Text;
using Domain.Entity.Reservation;

namespace Application.Dto.Output;

public static class ReservationDto
{
    public record Request(
        ulong Id,
        uint RoomId,
        DateTimeOffset CheckIn,
        DateTimeOffset CheckOut,
        byte ReservationStatusId,
        DateTimeOffset CreateAt,
        decimal TotalPrice
    )
    {
        public static Request FromReservation(Reservation reservation) => new(
            Id: reservation.Id,
            RoomId: reservation.RoomId,
            CheckIn: reservation.CheckIn,
            CheckOut: reservation.CheckOut,
            ReservationStatusId: reservation.ReservationStatusId,
            CreateAt: reservation.CreateAt,
            TotalPrice: reservation.TotalPrice
        );
    }
    public record Response(
        ulong Id,
        uint RoomId,
        DateTimeOffset CheckIn,
        DateTimeOffset CheckOut,
        byte ReservationStatusId,
        DateTimeOffset CreateAt,
        decimal TotalPrice
    )
    {
        public static Response FromReservation(Reservation reservation) => new(
            Id: reservation.Id,
            RoomId: reservation.RoomId,
            CheckIn: reservation.CheckIn,
            CheckOut: reservation.CheckOut,
            ReservationStatusId: reservation.ReservationStatusId,
            CreateAt: reservation.CreateAt,
            TotalPrice: reservation.TotalPrice
        );
    }
}