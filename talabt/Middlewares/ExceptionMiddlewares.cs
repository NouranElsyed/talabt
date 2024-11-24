using System.Text.Json;
using talabt.Error;

namespace talabt.Middlewares
{
    public class ExceptionMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddlewares> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddlewares(RequestDelegate next,ILogger<ExceptionMiddlewares> logger,IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                await  _next.Invoke(context);
            } 
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var Response =_env.IsDevelopment()? new ApiExceptionResponse(StatusCodes.Status500InternalServerError,ex.StackTrace.ToString(),ex.Message)
                    : new ApiExceptionResponse(StatusCodes.Status500InternalServerError, ex.StackTrace.ToString(), ex.Message);
                var option = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(Response,option);        
                context.Response.WriteAsync(json);
            }
        }
    }
}

