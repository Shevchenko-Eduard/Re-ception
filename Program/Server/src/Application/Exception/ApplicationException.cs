namespace Application.Exception;

public class ApplicationException : System.Exception
{
    public ApplicationException() : base() { }

    public ApplicationException(string? message) : base(message) { }

    public ApplicationException(string? message, System.Exception? innerException) : base(message, innerException) { }
}