using System.Text.RegularExpressions;
using Domain.Exception;

namespace Domain.Entity;
/// <summary>
/// Объект почты.
/// </summary>
public sealed partial class Email
{
    /// <summary>
    /// Регулярное выражение для проверки допустимости почты.
    /// </summary>
    private const string _regexEmailString = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    /// <summary>
    /// Создание регулярного выражения.
    /// </summary>
    [GeneratedRegex(_regexEmailString)]
    private static partial Regex RegexEmail();
    /// <summary>
    /// Поле для хранения почта.
    /// </summary>
    public string Value
    {
        get;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (!RegexEmail().IsMatch(value))
            {
                throw new DomainExternalException(message: "The email does not meet the specified requirements.");
            }
            field = value;
        }
    }
    /// <summary>
    /// Конструктор для почты.
    /// </summary>
    public Email(string value)
    {
        Value = value;
    }
    /// <summary>
    /// Вернуть в строковом представлении.
    /// </summary>
    public override string ToString() => Value;
    public override int GetHashCode() => Value.GetHashCode();
}