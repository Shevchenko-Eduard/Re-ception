using Domain.Interfaces.Exception;

namespace Application.Exception;

public class ApplicationExternalException : ApplicationException, IExternalException
{
    public ApplicationExternalException() : base() { }

    public ApplicationExternalException(string? message) : base(message) { }

    public ApplicationExternalException(string? message, System.Exception? innerException) : base(message, innerException) { }
}