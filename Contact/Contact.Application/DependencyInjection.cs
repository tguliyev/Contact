using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Contact.Application
{
    /// <summary>
    /// Dependency Injection proceses of Application layer
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}