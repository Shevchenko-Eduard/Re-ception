namespace Domain.Entity.Room
{
    public sealed class RoomTag
    {
        private const ushort _maxName = 50;
        private const ushort _maxDescription = 250;
        public ushort Id { get; init; }
        public string Name
        {
            get; private set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                if (value.Length > _maxName)
                {
                    throw new ArgumentException(message: $"The name must not exceed {_maxName} characters.");
                }
                field = value;
            }
        }
        public string Description
        {
            get; private set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                if (value.Length > _maxDescription)
                {
                    throw new ArgumentException(message: $"The description must not exceed {_maxDescription} characters.");
                }
                field = value;
            }
        }
#pragma warning disable CS9264
        private RoomTag() { }
#pragma warning restore CS9264
        public RoomTag(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}