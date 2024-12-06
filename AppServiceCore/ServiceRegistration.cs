using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppServiceCore
{
    public static class ServiceRegistration
    {
        public static IEnumerable<(Type Interface, Type Implementation)> GetServices()
        {
            // Get the current assembly
            var assembly = Assembly.GetExecutingAssembly();

            // Find all classes that have matching interfaces by naming convention
            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)        // Get all concrete classes && t.Name != IAutoTypeMapper
                .Select(t => new
                {
                    Implementation = t,
                    Interface = t.GetInterface($"I{t.Name}")   // Find the matching interface by convention
                })
                .Where(t => t.Interface != null)               // Ensure that a matching interface exists
                .Select(t => (t.Interface!, t.Implementation)) // Return as (Interface, Implementation) tuple and not an anonymous object with named fields (null-forgiving operator)
                .ToList();

            return serviceTypes;
        }
    }
}
