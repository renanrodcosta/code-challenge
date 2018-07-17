using Microsoft.Extensions.DependencyInjection;
using MinhaVida.CodeChallege.VaccinationManagement.API.Infrastructure;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
