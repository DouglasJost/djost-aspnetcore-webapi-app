
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
            //
            // See extension method ServiceCollectionExtensions.AddServicesWithDefaultConventions for implementation.
            builder.Services.AddServicesWithDefaultConventions(
                Assembly.GetExecutingAssembly(),
                Assembly.Load("AppServiceCore"),
                Assembly.Load("WeatherLibrary"),
                Assembly.Load("TestGorillaLibrary"),
                Assembly.Load("OpenAiChatCompletions"));

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Add JsonStringEnumConverter to handle enum string conversion
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
