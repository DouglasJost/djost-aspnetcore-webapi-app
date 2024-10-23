using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;

namespace DjostAspNetCoreWebServer
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServicesWithDefaultConventions(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                // Scan all classes that have a mathcing interface
                var types = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract)
                    .Select(t => new
                    {
                        Implementation = t,
                        Interface = t.GetInterface($"I{t.Name}")  // Match interface by convention
                    })
                    .Where(t => t.Interface != null);  // Make sure interface exists

                // Register each class and its interface
                foreach (var type in types)
                {
                    if (type != null && type.Interface != null)
                    {
                        // Reister as Transient : create new instance every time requested
                        services.AddTransient(type.Interface, type.Implementation);

                        // TODO : Support AddScoped and AddSingleton
                        //   AddScoped    - One instance per HTTP request
                        //   AddSingleton - One instance for the entire app
                    }
                }
            }
        }
    }
}
