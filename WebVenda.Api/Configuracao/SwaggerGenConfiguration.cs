using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebVenda.Api.Configuracao
{
    public static class SwaggerGenConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var version = configuration["Swagger:Version"];
            var title = configuration["Swagger:Title"];

            services.AddSwaggerGen(c => {
                c.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo() { Title = title, Version = version });
            });

            return services;
        }

        public static IApplicationBuilder AddSwaggerConfigurationApp(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "post API V1");
            });

            return app;
        }
    }
}