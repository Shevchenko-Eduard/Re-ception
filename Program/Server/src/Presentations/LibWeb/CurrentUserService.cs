using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LibWeb;

/// <summary>
/// Предварительно надо добавить builder.Services.AddHttpContextAccessor();
/// </summary>
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    public string? Id => User?.FindFirstValue("sub");
    
}
