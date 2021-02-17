using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebVenda.Api.Configuracao;
using WebVenda.Dal;
using WebVenda.Dto;
using WebVenda.Model;

namespace WebVenda.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceCollection Services { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            Services = services;
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("VendaInMemory"));
            services.AdicionarServicos();
            services.AddControllers();
            services.AddSwaggerConfiguration(Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            var _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VeiculoModel, VeiculoDto>().ReverseMap();
                cfg.CreateMap<VendedorModel, VendedorDto>().ReverseMap();
                cfg.CreateMap<RegistrarVendaModel, RegistrarVendaDto>().ReverseMap();
                cfg.CreateMap<VendaModel, VendaDto>().ReverseMap();
            });

            var _mapper = _mapperConfiguration.CreateMapper();
            services.AddSingleton(_mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.AddSwaggerConfigurationApp();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var context = Services.BuildServiceProvider().GetService<ApiContext>();
            AdicionarDadosTeste(context);
        }

        private void AdicionarDadosTeste(ApiContext context)
        {
            var _veiculo1 = new VeiculoModel()
            {
                AnoFabricacao = 2015,
                Codigo = 1,
                Marca = "Fiat",
                Modelo = "Palio"
            };

            context.Veiculos.Add(_veiculo1);

            var _veiculo2 = new VeiculoModel()
            {
                AnoFabricacao = 2016,
                Codigo = 2,
                Marca = "Ford",
                Modelo = "KA"
            };

            context.Veiculos.Add(_veiculo2);

            var _veiculo3 = new VeiculoModel()
            {
                AnoFabricacao = 2018,
                Codigo = 3,
                Marca = "Fiat",
                Modelo = "Uno"
            };

            context.Veiculos.Add(_veiculo3);

            var _vendedor1 = new VendedorModel()
            {
                Codigo = 1,
                Cpf = "123456789",
                Email = "jose@hotmail.com",
                Nome = "Jose da Silva"
            };

            context.Vendedores.Add(_vendedor1);

            var _vendedor2 = new VendedorModel()
            {
                Codigo = 2,
                Cpf = "1234567891",
                Email = "ana@hotmail.com",
                Nome = "Ana de Oliveira"
            };

            context.Vendedores.Add(_vendedor2);

            var _vendedor3 = new VendedorModel()
            {
                Codigo = 3,
                Cpf = "12345678912",
                Email = "bruno@hotmail.com",
                Nome = "Bruno Assis"
            };

            context.Vendedores.Add(_vendedor3);
            context.SaveChanges();
        }
    }
}