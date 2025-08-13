using Microsoft.AspNetCore.Http;

namespace Api_DGA.Middleware
{
    public class AuthenticationDebugMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationDebugMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            var method = context.Request.Method;
            
            Console.WriteLine($"🔍 Request: {method} {path}");
            
            // Verificar si hay token en el header
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader))
            {
                Console.WriteLine($"🔑 Authorization header present: {authHeader.StartsWith("Bearer ")}");
                if (authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring(7);
                    Console.WriteLine($"🔑 Token length: {token.Length}");
                }
            }
            else
            {
                Console.WriteLine($"❌ No Authorization header found");
            }

            // Verificar el estado de autenticación después del middleware de autenticación
            await _next(context);
            
            Console.WriteLine($"🔍 Response: {context.Response.StatusCode} for {method} {path}");
            
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                Console.WriteLine($"✅ User authenticated: {context.User.Identity.Name}");
            }
            else
            {
                Console.WriteLine($"❌ User not authenticated");
            }
        }
    }

    public static class AuthenticationDebugMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationDebug(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationDebugMiddleware>();
        }
    }
}
