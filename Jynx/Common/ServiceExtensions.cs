using System.Linq;
using System.Reflection;
using Jynx.Common.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Jynx.Common
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddJynxServices(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.GetCustomAttribute<JynxService>() != null && !x.IsAbstract);

            foreach (var type in types)
            {
                services.AddSingleton(type);
            }

            return services;
        }
    }
}