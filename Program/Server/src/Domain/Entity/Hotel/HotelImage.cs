namespace Domain.Entity.Hotel;

public class HotelImage
{
    #region Constants
    #endregion
    #region Fields
    public int Id { get; init; }
    public int HotelId { get; init; }
    public string ContentType { get; private set; }
    public string Extension { get; private set; }
    public Guid ImageKey { get; init; }
    #endregion
    #region Navigation properties
    public Hotel? Hotel { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
    private HotelImage() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
    public HotelImage(
        int hotelId,
        string extension,
        string contentType)
    {
        HotelId = hotelId;
        ContentType = contentType;
        Extension = extension;
        ImageKey = Guid.NewGuid();
    }
    #endregion
    #region Methods
    public void UpdateExtension(string extension)
    {
        Extension = extension;
    }
    public void UpdateContentType(string contentType)
    {
        ContentType = contentType;
    }
    #endregion
}