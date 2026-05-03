using Domain.Interfaces;

namespace Domain.Entity.Reservation;

public class Reservation
{
    #region Interfaces
    private readonly ICalculatorReservationPrice _calculatorPrice;
    #endregion
    #region Fields
    public int Id { get; init; }
    public string GuestId { get; init; }
    public int RoomId { get; init; }
    public DateTimeOffset CheckIn { get; private set; }
    public DateTimeOffset CheckOut { get; private set; }
    public byte ReservationStatusId { get; private set; }
    public DateTimeOffset CreateAt { get; init; }
    public decimal TotalPrice { get; private set; }
    public decimal? Discount { get; private set; }
    #endregion
    #region Navigation properties
    public Room.Room? Room
    {
        get; private set
        {
            if (value?.Id != RoomId)
            {
                throw new ArgumentException();
            }
            field = value;
        }
    }
    public ReservationStatus? ReservationStatus { get; private set; }
    public ICollection<Payment.Payment>? Payments { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS9264, CS8618
    private Reservation() { }
#pragma warning restore CS9264, CS8618
    public Reservation(
        ICalculatorReservationPrice calculatorPrice,
        string guestId, int roomId,
        DateTimeOffset checkIn, DateTimeOffset checkOut,
        int reservationStatusId = 0,
        decimal? discount = null)
    {
        _calculatorPrice = calculatorPrice;
        GuestId = guestId;
        RoomId = roomId;
        CheckIn = checkIn;
        CheckOut = checkOut;
        ReservationStatusId = (byte)reservationStatusId;
        Discount = discount;
    }
    #endregion
    #region Methods
    public void UpdateCheckIn(DateTimeOffset checkIn)
    {
        CheckIn = checkIn;
    }
    public void UpdateCheckOut(DateTimeOffset checkOut)
    {
        CheckOut = checkOut;
    }
    public void UpdateReservationStatusId(byte reservationStatusId)
    {
        ReservationStatusId = reservationStatusId;
    }
    public async Task UpdateTotalPrice()
    {
        TotalPrice = await _calculatorPrice.Calculator(this);
    }
    public void UpdateDiscount(decimal discount)
    {
        Discount = discount;
    }
    public void SetRoom(Room.Room room)
    {
        Room = room;
    }
    #endregion
}