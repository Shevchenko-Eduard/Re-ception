namespace Domain.Entity.Hotel;

public class HotelImage
{
    #region Constants
    #endregion
    #region Fields
    public int Id { get; init; }
    public int? HotelId { get; init; }
    public Guid ImageKey { get; init; }
    #endregion
    #region Navigation properties
    public Hotel? Hotel { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
    private HotelImage() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
    public HotelImage(int hotelId)
    {
        HotelId = hotelId;
        ImageKey = Guid.NewGuid();
    }
    #endregion
    #region Methods
    #endregion
}