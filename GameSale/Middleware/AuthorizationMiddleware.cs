using Microsoft.AspNetCore.Http.Features;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameSale.Middleware
{
    public class AuthorizationMiddleware
    {

        private readonly RequestDelegate _next;
        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var userCookie = httpContext.Request.Cookies["Auth_JWT"];

            var endpoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<MyAuthorizationAttribute>();

            if (attribute == null)
            {
                await _next(httpContext);
                return;
            }

            if (string.IsNullOrEmpty(userCookie))
            {
                httpContext.Response.Redirect("/");
                return;
            }
            var userRole = attribute.Role;

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtModel = jwtHandler.ReadJwtToken(userCookie);

            List<string> requestUserClaims = jwtModel.Claims.Where(x => x.Type == ClaimTypes.Role).Select(y => y.Value).ToList();

            if (!requestUserClaims.Contains(userRole))
            {
                httpContext.Response.Redirect("/");
                return;
            }


            await _next(httpContext);
        }
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthorizationMiddlewareExtantions
    {
        public static IApplicationBuilder AddAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }

}
