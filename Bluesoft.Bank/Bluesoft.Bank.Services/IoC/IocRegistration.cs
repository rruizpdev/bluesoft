using Bluesoft.Bank.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using RCRP.Common.IoC;

namespace Bluesoft.Bank.Services.IoC;

public static class IocRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        var servicesAssembly = typeof(AccountService).Assembly;
        services.RegisterServices(ServiceType.DomainService, servicesAssembly);

        var repositoriesAssembly = typeof(AccountRepository).Assembly;
        services.RegisterServices(ServiceType.Repository, repositoriesAssembly);
    }
}
