using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Newtonsoft.Json;
using Owin;
using OwinJWT;

[assembly: OwinStartup(typeof(Startup))]

namespace OwinJWT
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AllowedAudiences = new[] { JwtSettings.Audience },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(JwtSettings.Issuer, JwtSettings.Secret), 
                },
            });

            app.Map("/auth", builder => builder.Use(async (context, func) =>
            {
                context.Response.Headers["Content-Type"] = "application/json";

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, "SomeUser")
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    AccessToken = JwtTokenHelper.GetToken(claims)
                }));
            }));

            app.Use(async (context, func) =>
            {
                var user = context.Authentication.User;

                context.Response.Headers["Content-Type"] = "text/html";

                if (user.Identity.IsAuthenticated)
                {
                    await context.Response.WriteAsync("Authenticated: " + user.Identity.Name);
                }
                else
                {
                    await context.Response.WriteAsync("Not authenticated");
                }
            });
        }
    }
}