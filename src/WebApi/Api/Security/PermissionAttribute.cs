using Microsoft.AspNetCore.Mvc.Filters;

namespace Papirus.WebApi.Api.Security;

[ExcludeFromCodeCoverage]
public class PermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly string _permission;

    public PermissionAttribute(string permission = "")
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var _currentUserService = context.HttpContext.RequestServices.GetService<ICurrentUserService>();
        var _permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();
        var logger = context.HttpContext.RequestServices.GetService<ILogger<PermissionAttribute>>();

        bool userHasPermission = false;
        int userId = 0;
        if (_permissionService != null && _currentUserService != null)
        {
            userId = await _currentUserService.GetCurrentUserIdAsync();

            userHasPermission = await _permissionService.HasPermission(userId, _permission);
        }

        if (!userHasPermission)
        {
            context.Result = new StatusCodeResult(403);
            string logMessage = "User {UserId} does not have permission {Permission}.";
            logger?.LogWarning(logMessage, userId, _permission);
        }
    }
}