using CAMT.Domain.Config;
using CAMT.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CAMT.WebApi.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
    {
        services.AddLogging();
        services.AddHttpContextAccessor();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddTransient<GloblalExceptionHandlingMiddleware>();
        services.AddSingleton<TenantMiddlewareService>();

        var section = config.GetSection("JwtConfig");
        services.Configure<JwtConfig>(section);

        var configJwt = section.Get<JwtConfig>() ?? throw new ArgumentNullException(nameof(JwtConfig));

        var secretKey = Encoding.ASCII.GetBytes(configJwt.Secret ?? string.Empty);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
        {
            //Esto es para que solo me pida token en swagger y no bearer antes
            x.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                    if (authHeader != null && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        context.Token = authHeader.Substring("Bearer ".Length).Trim();
                    }
                    else if (authHeader != null)
                    {
                        context.Token = authHeader.Trim();
                    }
                    return Task.CompletedTask;
                }
            };
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });

        services.AddAuth();

        return services;
    }


    private static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddControllers(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                             .Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        });

        return services;
    }


    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(op =>
        {            
            op.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header without Bearer prefix. Example: 'Authorization: {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
            });

            op.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            },
                            Scheme = "apikey",
                            In = ParameterLocation.Header,
                            Name = "Authorization"
                        },
                        new string[] {}
                    }
                });
        });
        return services;
    }
}