
using DjostAspNetCoreWebServer.Authentication.CustomExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Events;
using Microsoft.AspNetCore.Http;
using Asp.Versioning;

namespace DjostAspNetCoreWebServer
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var builder = WebApplication.CreateBuilder(args);

            //
            // Logging URLS :
            // ==============
            //   Logging in .NET Core:
            //   https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-9.0
            //
            //   Third-party logging providers (Serilog.AspNetCore - NuGet package):
            //   https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-9.0#third-party-logging-providers
            //

            //
            // Use Serilog : https://serilog.net 
            //
            // Required NuGet packages for Startup Project (appsettings.json / appsettings.development.json) :
            // ===============================================================================================
            //   <PackageReference Include="Serilog.AspNetCore" Version="8.0.3" />
            //   <PackageReference Include="serilog.sinks.console" Version="6.0.0" />
            //   <PackageReference Include="serilog.sinks.file" Version="6.0.0" />
            //   <PackageReference Include="serilog.settings.configuration" Version="8.0.4" /> 
            //   <PackageReference Include="serilog.enrichers.environment" Version="3.0.1" />
            //   <PackageReference Include="serilog.exceptions" Version="8.4.0" />
            //   <PackageReference Include="Serilog.Enrichers.AspNetCore" Version="1.0.0" />
            //
            // Install using Package Manager Console :
            // =======================================
            //   PM> install-package serilog.AspNetCore
            //   PM> install-package serilog.sinks.console
            //   PM> install-package serilog.sinks.file
            //   PM> install-package serilog.Settings.Configuration
            //   PM> install-package serilog.enrichers.environment
            //   PM> install-package serilog.exceptions
            //   PM> install-package serilog.enrichers.aspnetcore
            //
            // Required NuGet packages for Class Libraries that are referenced :
            // =================================================================
            //  <PackageReference Include="Serilog" Version="4.1.0" />
            //  <PackageReference Include="Serilog.Extensions.Logging" Version="8.0.0" />
            //
            // Install using Package Manager Console :
            // =======================================
            //   PM> install-package Serilog
            //   PM> install-package Serilog.Extensions.Logging
            //
            // Example uses (also see LoginAuthenticationController) :
            // =======================================================
            //   var _logger = AppServiceCore.Loggers.AppSerilogLogger.GetLogger(AppServiceCore.Loggers.SerilogLoggerCategoryType.LoginAuthentication);
            //   var _logger = Log.ForContext("SourceContext", "LoginAuthentication");
            //   var _nonCategoryLogger = Log.Logger;
            //

            // Register IHttpContextAccessor in the service collection.
            // Needed for HttpRequestIpEnricher, so Serilog can capture IP address of caller.
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                // Reference appsettings.json and appsettings.Development.json for Serilog logger configuration.
                // Application supports multiple Serilog loggers.
                configuration.ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithExceptionDetails();

                // Apply category-specific Logging.
                var categoryLoggers = context.Configuration.GetSection("CategorySpecificLogging:Loggers").GetChildren();
                foreach (var categoryLogger in categoryLoggers)
                {
                    var name = categoryLogger.GetValue<string>("Name");
                    var filePath = categoryLogger.GetValue<string>("FilePath");
                    var consoleTemplate = categoryLogger.GetValue<string>("ConsoleOutputTemplate");
                    var fileTemplate = categoryLogger.GetValue<string>("FileOutputTemplate");
                    var filter = categoryLogger.GetValue<string>("Filter");

                    // Explicitly parse the MinimumLevel value
                    var minimumLevelString = categoryLogger.GetValue<string>("MinimumLevel");
                    LogEventLevel minimumLevel = Enum.TryParse(minimumLevelString, true, out LogEventLevel parsedLevel)
                        ? parsedLevel
                        : LogEventLevel.Information; // Fallback to Information if parsing fails

                    if (name == null || filePath == null || consoleTemplate == null || fileTemplate == null || filter == null)
                    {
                        continue;
                    }

                    configuration
                        .WriteTo.Logger(loggerConfig => loggerConfig
                            .MinimumLevel.Is(minimumLevel)
                            .Filter.ByIncludingOnly(logEvent => 
                                logEvent.Properties.ContainsKey("SourceContext") &&
                                logEvent.Properties["SourceContext"].ToString().Contains(filter))
                            // .Enrich.WithProperty("SourceContext", name)  // Use Enrich.WithProperty to add SourceContext for category-specific context
                            .WriteTo.Console(outputTemplate: consoleTemplate, restrictedToMinimumLevel: minimumLevel)
                            .WriteTo.File(filePath, outputTemplate: fileTemplate, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: minimumLevel));
                }
            });

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            // Add services to the DI container.
            // See extension method ServiceCollectionExtensions.AddServicesWithDefaultConventions for implementation.
            builder.Services.AddServicesWithDefaultConventions();

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Add JsonStringEnumConverter to handle enum string conversion.
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddControllers(options =>
            {
                // JSON is currently only supported output formatter.
                // Return HTTP 406 Not Acceptable response, if no formatter has been selected to format the response.
                options.ReturnHttpNotAcceptable = true;

                // Add AuthenticationExceptionFilter to handle authentication exceptions.
                options.Filters.Add<AuthenticationExceptionFilter>();
            });

            // Enable standardized exception error responses for HTTP APIs.  Includes type, title, status, detail, instance
            builder.Services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = ctx =>
                {
                    // Add server that threw exception to error response.
                    ctx.ProblemDetails.Extensions.Add("server", Environment.MachineName);
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //
            // Use JWT Bearer Token Authentication.
            // ====================================
            //   NuGet package : Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.11"
            //
            builder.Services.AddAuthentication("Bearer")
                // Configure the JWT.  Reference appsettings.json / appsettings.Development.json for JWT configuration.
                .AddJwtBearer(options =>
                    {
                        // secretForKey used assigning key object value for JWT validation to IssuerSigningKey.
                        var secretForKey = builder.Configuration["Authentication:SecretForKey"] 
                            ?? throw new BadRequestException("Authentication configuration values are missing");

                        // Validation rules for incoming tokens
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            // Verify tokens's iss (issuer) claim against trusted issuer.
                            ValidateIssuer = true,
                            // Verify tokens's aud (audience) claim matches this application's audience. 
                            ValidateAudience = true,
                            // Validate that the token's signature is correct by using a trusted signing key.
                            ValidateIssuerSigningKey = true,
                            // Only tokens issued by this issuer are accepted.
                            ValidIssuer = builder.Configuration["Authentication:Issuer"],
                            // Only tokens intended for this audience are accepted.
                            ValidAudience = builder.Configuration["Authentication:Audience"],
                            // This is the security key used to validate the token's signature.
                            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(secretForKey))
                        };
                    }
                );


            // Configure API Versioning
            builder.Services.AddApiVersioning(setupAction =>
            {
                setupAction.ReportApiVersions = true;
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new ApiVersion(1, 0);
            }).AddMvc();

            var app = builder.Build();

            // Initialize the AppLogger with the application's logger factory.  Microsoft.Extensions.Logging is the default.
            //   * Default Log Level is set in appsettings.Development.json / appsettings.json
            //   * LogLevels found in appsettings.json map to the custom category name of the ILogger.
            //   * Custom category names map to LoggerCategoryType.  See AppServiceCore.Logging.AppLogger.
            AppServiceCore.Logging.AppLogger.InitializeLogger(app.Services.GetRequiredService<ILoggerFactory>());

            // Singleton Application logger that encapsulates Serilog.  
            AppServiceCore.Loggers.AppSerilogLogger.InitializeLogger(Serilog.Log.Logger);

            // Serilog : capture caller's IP address to include in Serilog logs.
            //app.UseSerilogRequestLogging(options =>
            //{
            //    options.EnrichDiagnosticContext = (diagnosticContext, httpContect) =>
            //    {
            //        var ipAddress = httpContect.Connection.RemoteIpAddress?.ToString();
            //        diagnosticContext.Set("IPAddress", ipAddress);
            //    };
            //});

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();  

            app.MapControllers();

            app.Run();
        }
    }
}
