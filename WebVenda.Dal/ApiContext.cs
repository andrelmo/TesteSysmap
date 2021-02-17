using Microsoft.EntityFrameworkCore;
using WebVenda.Model;

namespace WebVenda.Dal
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
          : base(options)
        { 

        }

        public DbSet<VeiculoModel> Veiculos { get; set; }
        public DbSet<VendedorModel> Vendedores { get; set; }
        public DbSet<VendaModel> Vendas { get; set; }
    }
}