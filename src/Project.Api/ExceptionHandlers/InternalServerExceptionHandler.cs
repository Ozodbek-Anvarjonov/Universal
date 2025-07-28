﻿using Microsoft.AspNetCore.Diagnostics;
using Project.Application.Common.Exceptions;
using Project.Application.Common.Response;
using System.Net;

namespace Project.Api.ExceptionHandlers;

public class InternalServerExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var currentException = new CustomException(exception.Message, HttpStatusCode.InternalServerError);

        httpContext.Response.StatusCode = (int)currentException!.StatusCode;
        httpContext.Response.ContentType = "application/json";

        var response = new ApiErrorResponse
        {
            Type = currentException.Type,
            Status = (int)currentException.StatusCode,
            Title = currentException.Title,
            Detail = currentException.Detail
        };

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}