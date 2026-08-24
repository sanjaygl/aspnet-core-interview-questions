using API.Database.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Scans the provided assembly (or the executing assembly) for concrete classes
        // that have a matching interface named I{ClassName} and registers them as transient.
        public static IServiceCollection AddServices(this IServiceCollection services, Assembly? assembly = null)
        {
            assembly ??= Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes()
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    !t.IsGenericTypeDefinition);

            foreach (var impl in types)
            {
                var ifaceName = "I" + impl.Name;
                var iface = impl.GetInterface(ifaceName);

                if (iface != null)
                {
                    services.AddScoped(iface, impl);
                }
            }

            services.AddScoped<PasswordHasher<User>>();

            return services;
        }
    }
}
