using System.Text.RegularExpressions;

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
                throw new ArgumentException(message: "The email does not meet the specified requirements.");
            }
            field = value;
        }
    }
    /// <summary>
    /// Верифицирована ли почта.
    /// </summary>
    public bool IsVerified { get; private set; }
    /// <summary>
    /// Конструктор для почты.
    /// </summary>
    public Email(string value)
    {
        Value = value;
    }
    /// <summary>
    /// Утверждает что почта верифицирована.
    /// </summary>
    public void Verified() => IsVerified = true;
    /// <summary>
    /// Вернуть в строковом представлении.
    /// </summary>
    public override string ToString() => Value;
}