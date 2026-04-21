namespace WebApi;

public class HostEnvironment(IHostEnvironment hostEnvironment) : Infrastructure.Interfaces.IHostEnvironment
{
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;

    public string CurrentEnvironment => _hostEnvironment.EnvironmentName;

    public bool IsDevelopment()
    {
        return _hostEnvironment.IsDevelopment();
    }

    public bool IsProduction()
    {
        return _hostEnvironment.IsProduction();
    }

    public bool IsStaging()
    {
        return _hostEnvironment.IsStaging();
    }
}