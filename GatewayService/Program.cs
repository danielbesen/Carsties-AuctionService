using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var identityServiceUrl = Environment.GetEnvironmentVariable("IdentityServiceUrl");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = identityServiceUrl,
            ValidateAudience = false,
            ValidateIssuerSigningKey = false,
            NameClaimType = "username",
            SignatureValidator = delegate (string token, TokenValidationParameters parameters)
            {
                var jwt = new JsonWebToken(token);

                return jwt;
            },
        };
        options.RequireHttpsMetadata = false;
    });

var app = builder.Build();

app.MapReverseProxy();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
