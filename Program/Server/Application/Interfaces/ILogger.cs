namespace Application.Interfaces;

public interface ILogger<T>
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(Exception ex, string message);
}