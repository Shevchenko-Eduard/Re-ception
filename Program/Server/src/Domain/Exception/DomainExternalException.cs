using Domain.Interfaces.Exception;

namespace Domain.Exception;

public class DomainExternalException : DomainException, IExternalException
{
    public DomainExternalException() : base() { }

    public DomainExternalException(string? message) : base(message) { }

    public DomainExternalException(string? message, System.Exception? innerException) : base(message, innerException) { }
}