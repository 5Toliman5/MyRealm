using MyRealm.Common.Exceptions;
using MyRealm.Common.Middleware;
using System.ComponentModel.DataAnnotations;

namespace MyRealm.Messaging.Api.Middleware
{
    public class MessagingApiErrorHandlerMiddleware : ErrorHandlerMiddleware
    {
        public MessagingApiErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger) : base(next, logger)
        {
        }
        protected override int HandleException(Exception ex)
        {
            switch (ex)
            {
                case KeyNotFoundException:
                case ValidationException:
                    this.Logger.LogError(default(EventId), ex.Message);
                    return StatusCodes.Status400BadRequest;
                default:
                    this.Logger.LogCritical(ex, "Critical error");
                    return StatusCodes.Status500InternalServerError;
            }
        }
    }
}
