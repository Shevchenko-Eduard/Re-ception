using System.Text.RegularExpressions;

namespace Domain;
/// <summary>
/// Объект номера телефона.
/// </summary>
public partial class Phone
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
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (!PhoneRegex().IsMatch(value))
            {
                throw new ArgumentException(message: "Does not meet standards.");
            }
            field = value;
        }
    }
    /// <summary>
    /// Верифицирован ли номер телефона.
    /// </summary>
    public bool IsVerified { get; private set; }
    /// <summary>
    /// Конструктор для номера телефона.
    /// </summary>
    public Phone(string value)
    {
        Value = value;
    }
    /// <summary>
    /// Утверждает что номер телефона верифицирован.
    /// </summary>
    public void Verified() => IsVerified = true;
    /// <summary>
    /// Вернуть в строковом представлении.
    /// </summary>
    public override string ToString() => Value;
}