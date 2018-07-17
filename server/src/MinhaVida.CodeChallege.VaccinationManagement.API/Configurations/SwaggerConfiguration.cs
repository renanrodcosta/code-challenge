using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Vaccination Management API",
                    Contact = new Contact { Name = "Renan Costa", Email = "renan.rodcosta@gmail.com" }
                });
            });
        }
    }
}
