using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BasePizzaSystemException basePizzaSystemException)
            {
                await HandleExceptionAsync(context, basePizzaSystemException);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, new UnhandelledException(ex.Message));
            }
        }
        private Task HandleExceptionAsync(HttpContext context, BasePizzaSystemException exception)
        {
            var exceptionResponseResult = JsonSerializer.Serialize(new
            {
                error = exception.GetFormatedMessage(),
                errorType = exception.GetErrorType(),
                statusCode = exception.GetHttpStatusCode(),
                errorLevelType = exception.GetErrorLevelType()

            });
            var exceptionLogResult = JsonSerializer.Serialize(new
            {
                error = exception.GetFormatedMessage(),
                errorType = exception.GetErrorType(),
                statusCode = exception.GetHttpStatusCode(),
                errorLevelType = exception.GetErrorLevelType(),
                stackTrace = exception.StackTrace
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.GetHttpStatusCode();
            _logger.LogError(exceptionLogResult);
            return context.Response.WriteAsync(exceptionResponseResult);
        }
    }
}
