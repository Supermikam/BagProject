using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using BagProject.SimpleTokenProvider;
using Microsoft.Extensions.Options;
using BagProject.Models;
using Microsoft.Extensions.Logging;

namespace BagProject
{
    public partial class Startup
    {
        private static readonly string secretKey = "mysupersecret_secretkey!123";
        private void ConfigureAuth(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var _userManager = app.ApplicationServices.GetRequiredService<UserManager<AppUser>>();
                var _signInManager = app.ApplicationServices.GetRequiredService<SignInManager<AppUser>>();
           

                var testUser = _userManager.FindByNameAsync("liuruohannah@gmail.com").Result;

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
                var options = new TokenProviderOptions
                {
                    Path = "/api/token",
                    Audience = "AppUser",
                    Issuer = "QualityBags",
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                    IdentityResolver =  GetIdentity
                };
                app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));


                var tokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = "QualityBags",

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = "AppUser",

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



                async Task<ClaimsIdentity> GetIdentity(string username, string password)
                {
                    var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByEmailAsync(username);
                        var claims = await _userManager.GetClaimsAsync(user);

                        return new ClaimsIdentity(new GenericIdentity(username, "Token"), claims);
                    }

                    // Credentials are invalid, or account doesn't exist
                   
                    return null;

                }
            }
            //private Task<ClaimsIdentity> GetIdentity(string username, string password)
            //{
            //    // Don't do this in production, obviously!
            //    if (username == "TEST" && password == "TEST123")
            //    {
            //        return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            //    }

            //    // Credentials are invalid, or account doesn't exist
            //    return Task.FromResult<ClaimsIdentity>(null);
            //}
        }
    }
}
