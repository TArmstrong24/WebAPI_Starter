using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http.Internal;

namespace StarterAPI.AppCode
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {   
            //Workaround - copy original Stream
            _logger.LogInformation("Handling request: " + context.Request.Path);

            var initalBody = context.Request.Body;

            using (var bodyReader = new StreamReader(context.Request.Body))
            {
                string body = await bodyReader.ReadToEndAsync();

                _logger.LogInformation("Request body: " + body);

                byte[] requestData = Encoding.UTF8.GetBytes(body);

                context.Request.Body = new MemoryStream(requestData);
            }

            //handle other middlewares
            await _next.Invoke(context);

            context.Request.Body = initalBody;

            _logger.LogInformation("Finished handling request.");
        }
    }

    public static class RequestLoggerExtensions
    {
        public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggerMiddleware>();
        }

    }
}
