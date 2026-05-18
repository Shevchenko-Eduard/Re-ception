using Domain.Interfaces.Exception;

namespace Infrastructure.Exception;

public class InfrastructureInnerException : InfrastructureException, IInnerException
{
    public InfrastructureInnerException() : base() { }
    public InfrastructureInnerException(string? message) : base(message: message) { }
    public InfrastructureInnerException(string? message, System.Exception innerException) : base(message: message, innerException: innerException) { }
}