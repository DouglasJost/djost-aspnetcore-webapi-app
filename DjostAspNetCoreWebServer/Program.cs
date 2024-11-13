
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace DjostAspNetCoreWebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the DI container
            // See extension method ServiceCollectionExtensions.AddServicesWithDefaultConventions for implementation.
            builder.Services.AddServicesWithDefaultConventions();

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Add JsonStringEnumConverter to handle enum string conversion
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // JSON is currently only supported output formatter
            builder.Services.AddControllers(options =>
            {
                //  return HTTP 406 Not Acceptable response, if no formatter has been selected to format the response
                options.ReturnHttpNotAcceptable = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //
            // Add logging configuration
            //
            // NOTE: Default Log Level is set in appsettings.Development.json and appsettings.json
            // 
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var app = builder.Build();

            // Initialize the AppLogger with the application's logger factory
            AppServiceCore.Logging.AppLogger.InitializeLogger(app.Services.GetRequiredService<ILoggerFactory>());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();  

            app.MapControllers();

            app.Run();
        }
    }
}
