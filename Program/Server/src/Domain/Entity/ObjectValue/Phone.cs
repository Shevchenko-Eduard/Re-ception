using System.Text.RegularExpressions;
using Domain.Exception;

namespace Domain.Entity;
/// <summary>
/// Объект номера телефона.
/// </summary>
public sealed partial class Phone
{
    /// <summary>
    /// Регулярное выражение для проверки допустимости номера телефона.
    /// </summary>
    private const string _regexPhoneString = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
    /// <summary>
    /// Создание регулярного выражения.
    /// </summary>
    [GeneratedRegex(_regexPhoneString)]
    private static partial Regex PhoneRegex();
    /// <summary>
    /// Поле для хранения номера телефона.
    /// </summary>
    public string Value
    {
        get;
        init
        {
            try
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                if (!PhoneRegex().IsMatch(value))
                {
                    throw new DomainExternalException(message: "Does not meet standards.");
                }
                field = value;
            }
            catch (System.Exception ex)
            {
                throw new DomainExternalException(message: ex.Message, innerException: ex);
            }
        }
    }
    /// <summary>
    /// Конструктор для номера телефона.
    /// </summary>
    public Phone(string value)
    {
        Value = value;
    }
    /// <summary>
    /// Вернуть в строковом представлении.
    /// </summary>
    public override string ToString() => Value;
}