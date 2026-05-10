using Domain.Entity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converter;

public class EmailToStringConverter: ValueConverter<Email, string>
{
    public EmailToStringConverter()
        : base(
            v => v.ToString(),           // Convert to string
            v => new (v)           // Convert to Email
    ){}
}