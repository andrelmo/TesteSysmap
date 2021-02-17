using Microsoft.Extensions.DependencyInjection;
using WebVenda.Dal.Implementation;
using WebVenda.Dal.Interface;

namespace WebVenda.Api.Configuracao
{
    public static class ConfiguracaoServicos
    {
        public static void AdicionarServicos(this IServiceCollection services)
        {
            services.AddTransient<IVendaDal, VendaDal>();
            services.AddTransient<IVendedorDal, VendedorDal>();
            services.AddTransient<IVeiculoDal, VeiculoDal>();
        }
    }
}