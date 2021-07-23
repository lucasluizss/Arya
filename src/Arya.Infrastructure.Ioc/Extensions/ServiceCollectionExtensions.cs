using Arya.Domain.Interfaces;
using Arya.Infrastructure.CrossCutting.Email;
using Arya.Infrastructure.Data.Context;
using Arya.Infrastructure.Data.UnitOfWork;
using GoldenCompany;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Arya.Infrastructure.Ioc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void InitializeMySqlDataBase(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

#if DEBUG
            services.AddDbContext<MySqlContext>(options => options.UseInMemoryDatabase("AryaDB"));
#else
            services.AddDbContext<MySqlContext>(options => options
            .UseMySql(connectionString, providerOptions =>
            {
                providerOptions.CommandTimeout(60);
                providerOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), null);
            })
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
#endif
        }

        public static void AddJwt(this IServiceCollection services, string securityKey)
        {
            var key = Encoding.ASCII.GetBytes(securityKey);

            Cryptography.SetKey(securityKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Arya.API",
                        Version = "v1",
                        Description = "API REST criada com o ASP.NET Core",
                        TermsOfService = new Uri("https://github.com/lucasluizss"),
                        Contact = new OpenApiContact
                        {
                            Name = "Lucas Silva",
                            Email = string.Empty,
                            Url = new Uri("https://twitter.com/lucasluizss"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT",
                            Url = new Uri("https://github.com/lucasluizss/Arya.API/blob/master/LICENSE"),
                        }
                    });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @$"JWT Authorization header using the Bearer scheme. {Environment.NewLine}
                                     Enter 'Bearer' [space] and then your token in the text input below. {Environment.NewLine}
                                     Example: Bearer 12345abcdef",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }});

                var appPath = PlatformServices.Default.Application.ApplicationBasePath;
                var appName = PlatformServices.Default.Application.ApplicationName;
                var xmlDocPath = Path.Combine(appPath, $"{appName}.xml");

                options.IncludeXmlComments(xmlDocPath);
            });
        }

        public static void AddEmail(this IServiceCollection services)
        {
            services.AddSingleton<IEmailService, EmailService>();
        }
    }
}
