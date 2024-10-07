using Microsoft.AspNetCore.Http;

namespace SharedLibrary_E_commerce.Middleware
{
    public class ListenToOnlyApiGateway (RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            //Extract specific header from the request
            var signedHeader = context.Request.Headers["Api-Gateaway"];

            //NULL means, the request is not coming from Api Gateway // 503 Service Unavailable
            if(signedHeader.FirstOrDefault() is null) 
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Sotty, service is unavailable");
                return;
            }
            else
            {
                await next(context);
            }
        }
    }
}
