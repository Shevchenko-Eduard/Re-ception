namespace Domain.Entity.User.Role;

public sealed class Role
{
    #region Constants
    private const ushort _maxDescription = 100;
    private const ushort _maxName = 50;
    #endregion
    #region Fields
    public ushort Id { get; init; }
    public string Name
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxName)
            {
                throw new ArgumentException(message: $"The name should not exceed {_maxName} characters.");
            }
            field = value;
        }
    }
    public string? Description
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxDescription)
            {
                throw new ArgumentException(message: $"The description should not exceed {_maxDescription} characters.");
            }
            field = value;
        }
    }
    #endregion
    #region Navigation properties
    public IEnumerable<Employee.Employee>? Employees { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS9264
    private Role() { }
#pragma warning restore CS9264
    public Role(string name) => Name = name;
    public Role(string name, string? description) : this(name)
    {
        if (description is not null)
        {
            Description = description;
        }
    }
    #endregion
    #region Methods
    public void UpdateName(string name) => Name = name;
    public void UpdateDescription(string description) => Description = description;
    #endregion
}