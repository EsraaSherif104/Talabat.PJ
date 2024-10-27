using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Talabt.APIS.Errors;
namespace Talabt.APIS.MiddelWire
{
    public class ExceptionMiddlewire
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddlewire> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddlewire(RequestDelegate Next,ILogger<ExceptionMiddlewire>logger,IHostEnvironment environment)
        {
            _next = Next;
            this._logger = logger;
            this._environment = environment;
        }
        //invokeAsync
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
             await   _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                //production=>log ex in DB
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //if (_environment.IsDevelopment())
                //{
                //    var respone = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());
                //}
                //else
                //{
                //    var response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                //}
                var response=_environment.IsDevelopment()? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonResponse=JsonSerializer.Serialize(response);
               
                context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
