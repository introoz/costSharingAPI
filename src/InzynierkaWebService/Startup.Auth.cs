﻿using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Principal;
using SimpleTokenProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTokenProvider.Test;
using InzynierkaWebService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace InzynierkaWebService
{
    public partial class Startup
    {
        // The secret key every token will be signed with.
        // Keep this safe on the server!

        private void ConfigureAuth(IApplicationBuilder app)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("SecretStrings")["SecretKey"]));

            //var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));                        

            var context = app.ApplicationServices.GetService<CostSharingContext>();

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/api/token",
                Audience = "ExampleAudience",
                Issuer = "ExampleIssuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = (username, password) => GetIdentity(context, username, password)
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "ExampleIssuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "ExampleAudience",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = "access_token",
                TicketDataFormat = new CustomJwtDataFormat(
                    SecurityAlgorithms.HmacSha256,
                    tokenValidationParameters)
            });
        }

        private Task<ClaimsIdentity> GetIdentity(CostSharingContext context, string username, string password)
        {
            // Don't do this in production, obviously!
            //if (username == "TEST" && password == "TEST123")
            //{
            //    return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            //}

            //// Credentials are invalid, or account doesn't exist
            //return Task.FromResult<ClaimsIdentity>(null);

            var user = context.Users.FirstOrDefault(u => u.Login == username && u.Password == password);
            if (user != null)
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            else
                return Task.FromResult<ClaimsIdentity>(null);

        }
    }
}
