using Domain.Interfaces.Repositories.RoomRepository;

namespace Domain.Entity.Reservation;

public class Reservation
{
    #region Fields
    public ulong Id { get; init; }
    public Guid GuestId { get; init; }
    public ushort RoomId { get; init; }
    public DateTimeOffset CheckIn { get; private set; }
    public DateTimeOffset CheckOut { get; private set; }
    public byte ReservationStatusId { get; private set; }
    public DateTimeOffset CreateAt { get; init; }
    public decimal TotalPrice { get; private set; }
    public decimal? Discount { get; private set; }
    #endregion
    #region Navigation properties
    public Guest.Guest? Guest { get; private set; }
    public Room.Room? Room { get; private set; }
    public ReservationStatus? ReservationStatus { get; private set; }
    #endregion
    #region Constructors
    private Reservation() { }
    public Reservation(
        Guid guestId, uint roomId,
        DateTimeOffset checkIn, DateTimeOffset checkOut,
        int reservationStatusId = 0,
        decimal? discount = null)
    {
        GuestId = guestId;
        RoomId = (ushort)roomId;
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
    public async Task UpdateTotalPrice(
        IRoomRepository roomRepository,
        IRoomTypeRepository roomTypeRepository)
    {
        TotalPrice = await CalculateTotalPrice(
            roomRepository, roomTypeRepository);
    }
    public void UpdateDiscount(decimal discount)
    {
        Discount = discount;
    }
    #region private
    private async Task<decimal> CalculateTotalPrice(
        IRoomRepository roomRepository,
        IRoomTypeRepository roomTypeRepository)
    {
        Room ??= await roomRepository.GetByIdAsync(RoomId)
            ?? throw new SystemException();
        if (Room.PricePerDay is null && Room.RoomType is null)
        {
            Room.SetRoomType(await roomTypeRepository.GetByIdAsync(Room.RoomTypeId)
                ?? throw new SystemException());
        }
        var pricePerDay = Room.PricePerDay is null
            ? Room.RoomType!.BasePricePerDay
            : Room.PricePerDay
            ?? throw new SystemException();
        var reservationTimeSpan = CheckOut - CheckIn;
        var reservationDays = (decimal)reservationTimeSpan.TotalDays;
        var totalPrice = reservationDays * pricePerDay;
        if (Discount is not null)
        {
            totalPrice *= (decimal)(1 - Discount);
        }
        return totalPrice;
    }
    #endregion  
    #endregion
}