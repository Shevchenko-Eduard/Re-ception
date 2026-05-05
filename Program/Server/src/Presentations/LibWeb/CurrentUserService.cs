using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LibWeb;

/// <summary>
/// Предварительно надо добавить builder.Services.AddHttpContextAccessor();
/// </summary>
public class CurrentUserService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly string _userIdClaimType = configuration["Auth:UserIdClaimType"] ?? "sub";

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    public string? Id => User?.FindFirstValue("sub");
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;    
}
