namespace Domain.Entity.Hotel;

public class Image
{
    #region Constants
    #endregion
    #region Fields
    public int Id { get; init; }
    public int HotelId { get; init; }
    public string ImageKey { get; init; }
    public byte[]? Bytes { get; private set; } = null;
    #endregion
    #region Navigation properties
    public Hotel? Hotel { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
    private Image() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
    public Image(int hotelId, string imageKey)
    {
        HotelId = hotelId;
        ImageKey = imageKey;
    }
    #endregion
    #region Methods
    public void UpdateBytes(byte[]? bytes) => Bytes = bytes;
    #endregion
}