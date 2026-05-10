using Domain.Entity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converter;

public class PhoneToStringConverter: ValueConverter<Phone, string>
{
    public PhoneToStringConverter()
        : base(
            v => v.ToString(),           // Convert to string
            v => new (v)           // Convert to Phone
    ){}
}