namespace Domain.Exception;

public class DomainInnerException: DomainException
{
    public DomainInnerException() : base()
    {
    }

    public DomainInnerException(string? message) : base(message)
    {
    }

    public DomainInnerException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}