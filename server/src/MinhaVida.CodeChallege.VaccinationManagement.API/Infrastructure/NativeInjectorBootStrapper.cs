using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Infrastructure
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(svc => svc.GetService<IHttpContextAccessor>().HttpContext);
        }
    }
}
