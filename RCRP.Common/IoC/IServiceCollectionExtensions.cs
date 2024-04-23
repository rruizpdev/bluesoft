using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

# nullable disable

namespace RCRP.Common.IoC;

public static class IServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services,
        ServiceType serviceType,
        Assembly assembly)
    {
        var serviceSuffix = serviceType switch
        {
            ServiceType.Repository => "Repository",
            ServiceType.DomainService => "Service",
            _ => throw new ArgumentException("Invalid service type.", nameof(serviceType))
        };

        var repositories = assembly.ExportedTypes.Where(t => t.IsClass
            && t.IsPublic
            && t.GetInterfaces().Any(i => i.Name.EndsWith(serviceSuffix))).ToList();

        foreach (var repository in repositories) 
        {
            services = services.AddScoped(repository.GetInterface($"I{repository.Name}"), repository);
        }
    }
}
