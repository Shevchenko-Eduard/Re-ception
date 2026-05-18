namespace Infrastructure.Exception;

public class InfrastructureException : System.Exception
{
    public InfrastructureException() : base() { }
    public InfrastructureException(string? message) : base(message: message) { }
    public InfrastructureException(string? message, System.Exception innerException) : base(message: message, innerException: innerException) { }
}