using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmartfaceSolution.Helpers;

namespace SmartfaceSolution.Middleware
{
    /// <summary>
    /// <c>ErrorHandlerMiddleware</c> it is a middleware that handles all exceptions
    /// and return the HTTP code based on the exception
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware>logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError(ex.Message);
                var response = context.Response;
                response.ContentType = "application/json";
                switch(ex)
                {
                    case UnauthorizedAccessException e:
                        response.StatusCode = (int) HttpStatusCode.Unauthorized;
                        break;
                    case AppException e:
                        // custom error, error code 400
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error, error code 404
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // internal error, error code 500
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = ex.Message });
                await response.WriteAsync(result);
            }
        }
    }
}