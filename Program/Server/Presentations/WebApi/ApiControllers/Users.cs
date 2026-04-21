using Microsoft.AspNetCore.Mvc;

namespace WebApi.ApiControllers;

[Route("api/v1/[controller]")]
public class UsersController(ILogger<UsersController> logger) : Controller
{
    private readonly ILogger<UsersController> _logger = logger;

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Received request to get users");
        // Здесь будет логика получения пользователей
        var users = new[]
        {
            new { Id = 1, Name = "Alice" },
            new { Id = 2, Name = "Bob" }
        };
        return Ok(users);
    }
}