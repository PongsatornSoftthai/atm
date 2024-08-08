using backend.Extensions;
using backend.Models;
using Logger.Service;
using System.Net;
using System.Text.Json;



namespace ST_API.Extensions.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerManager logger)
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
            catch (Exception error)
            {
                _logger.LogError($"Somthing went wrong : {error}");
                await HandleExceptionAsync(context, error);
            }
        }
        /// <summary>
        /// Error Message ที่แจ้งกลับ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            context.Response.ContentType = "application/json";
            var errorResponse = new ErrorDetails();

            switch (error)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid token"))
                    {
                        errorResponse.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.Message = ex.Message;
                        break;
                    }
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;
                //ไม่เจอข้อมูล
                case KeyNotFoundException ex:
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = ex.Message;
                    break;
                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal Server Error Check Log";
                    break;
            }
            _logger.LogError(error.Message);
            var result = JsonSerializer.Serialize(errorResponse);

            #region Save To DB
            //Systemfunction.onSaveLog_SystemError(null,null, error, "", "SYS", context.Request.Path);
            #endregion
            context.Response.StatusCode = errorResponse.StatusCode;
            await context.Response.WriteAsync(result);

        }
    }
}
