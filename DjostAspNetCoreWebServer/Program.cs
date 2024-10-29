
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
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

            var app = builder.Build();

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
