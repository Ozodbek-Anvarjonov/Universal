using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Project.Application.Common.Exceptions;
using Project.Application.Common.Response;
using System.Net;

namespace Project.Api.Filters;

public class CustomAuthorize : Attribute, IAsyncAuthorizationFilter
{
    private readonly string[] _roles;

    public CustomAuthorize(params string[] roles) =>
        _roles = roles ?? Array.Empty<string>();

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var actionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

        var allowAnonymous = actionDescriptor?.MethodInfo.GetCustomAttributes(inherit: true)
            .OfType<AllowAnonymousAttribute>().Any() ?? false;
        if (allowAnonymous) return;

        var user = context.HttpContext.User;

        if (user?.Identity?.IsAuthenticated is not true)
        {
            context.Result = BuildErrorResult(new CustomException(
                "Authentication is required to access this resource.",
                HttpStatusCode.Unauthorized));

            return;
        }

        if (_roles.Length > 0 && !_roles.Any(user.IsInRole))
        {
            context.Result = BuildErrorResult(new CustomException(
                "You do not have permission for this method.",
                HttpStatusCode.Forbidden));

            return;
        }

        await Task.CompletedTask;
    }

    private static ObjectResult BuildErrorResult(CustomException exception)
    {
        var response = new ApiErrorResponse
        {
            Type = exception.Type,
            Status = (int)exception.StatusCode,
            Title = exception.Title,
            Detail = exception.Detail
        };

        return new ObjectResult(response)
        {
            StatusCode = response.Status
        };
    }
}