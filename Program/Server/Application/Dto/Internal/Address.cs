namespace Application.Dto.Internal;

public static class AddressDto
{
    public record Value(
        string Country,
        string Region,
        string City,
        string Street,
        string Address
    );
}