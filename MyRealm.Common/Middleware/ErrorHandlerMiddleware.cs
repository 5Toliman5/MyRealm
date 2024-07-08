using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyRealm.Common.Exceptions;
using Newtonsoft.Json;
namespace MyRealm.Common.Middleware
{
    public class ErrorHandlerMiddleware
    {
        protected readonly RequestDelegate Next;
        protected readonly ILogger<ErrorHandlerMiddleware> Logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            this.Next = next;
            this.Logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.Next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = HandleException(ex);
                var error = new { error = ex?.Message };
                var result = JsonConvert.SerializeObject(error);
                await response.WriteAsync(result);
            }
        }
        protected virtual int HandleException(Exception ex)
        {
            switch (ex)
            {
                case NotFoundException:
                    this.Logger.LogError(default(EventId), ex.Message);
                    return StatusCodes.Status404NotFound;
                default:
                    this.Logger.LogCritical(ex, "Critical error");
                    return StatusCodes.Status500InternalServerError;
            }
        }
    }
}
