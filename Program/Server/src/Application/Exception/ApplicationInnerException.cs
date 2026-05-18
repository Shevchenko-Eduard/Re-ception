using Domain.Interfaces.Exception;

namespace Application.Exception;

public class ApplicationInnerException : ApplicationException, IInnerException
{
    public ApplicationInnerException() : base() { }

    public ApplicationInnerException(string? message) : base(message) { }

    public ApplicationInnerException(string? message, System.Exception? innerException) : base(message, innerException) { }
}