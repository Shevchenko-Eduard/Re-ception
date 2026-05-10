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
    public string? Value
    {
        get => _value;
        private set
        {
            try
            {
                if (value is not null && !RegexEmail().IsMatch(value))
                {
                    throw new DomainExternalException(message: "The email does not meet the specified requirements.");
                }
                _value = value;
            }
            catch (System.Exception ex)
            {
                throw new DomainExternalException(message: ex.Message, innerException: ex);
            }
        }
    }
    private string? _value;
    /// <summary>
    /// Конструктор для почты.
    /// </summary>
    public Email(string? value)
    {
        Value = value;
    }
    /// <summary>
    /// Вернуть в строковом представлении.
    /// </summary>
    public override string ToString() => Value ?? string.Empty;
    public override int GetHashCode() => Value?.GetHashCode() ?? 0;
}