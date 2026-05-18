using Domain.Interfaces.Exception;

namespace Infrastructure.Exception;

public class InfrastructureExternalException : InfrastructureException, IExternalException
{
    public InfrastructureExternalException() : base() { }
    public InfrastructureExternalException(string? message) : base(message: message) { }
    public InfrastructureExternalException(string? message, System.Exception innerException) : base(message: message, innerException: innerException) { }
}