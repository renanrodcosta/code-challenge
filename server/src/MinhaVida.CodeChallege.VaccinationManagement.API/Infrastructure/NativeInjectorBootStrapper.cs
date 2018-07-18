using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Repositories;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Infrastructure
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(svc => svc.GetService<IHttpContextAccessor>().HttpContext);

            services.AddScoped<PeopleRepository>();
        }
    }
}
