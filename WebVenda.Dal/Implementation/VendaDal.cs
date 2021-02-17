using System.Threading.Tasks;
using WebVenda.Dal.Interface;
using WebVenda.Enumeradores;
using WebVenda.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebVenda.Dal.Implementation
{
    public sealed class VendaDal : IVendaDal
    {
        private readonly ApiContext _apiContext;
        private readonly IVendedorDal _vendedorDal;
        private readonly IVeiculoDal _veiculoDal;

        public VendaDal(ApiContext apiContext, IVendedorDal vendedorDal, IVeiculoDal veiculoDal)
        {
            this._apiContext = apiContext;
            this._vendedorDal = vendedorDal;
            this._veiculoDal = veiculoDal;
        }

        public async Task<RegistrarVendaModel> Inserir(RegistrarVendaModel venda)
        {
            var _vendaModel = new VendaModel()
            {
                DataVenda = venda.DataVenda,
                Id = GeradorIdVenda.GerarNovoId(),
                Status = venda.Status,
                VendedorId = venda.CodigoVendedor
            };

            _vendaModel.Vendedor = this._apiContext.Vendedores.Where(i => i.Codigo == venda.CodigoVendedor).Single();

            foreach (var _veiculoId in venda.ListaVeiculos)
                _vendaModel.ListaVeiculos.Add(this._apiContext.Veiculos.Where(i => i.Codigo == _veiculoId).Single());

            await this._apiContext.Vendas.AddAsync(_vendaModel);
            await this._apiContext.SaveChangesAsync();

            venda.Id = _vendaModel.Id;

            return (venda);
        }

        public async Task AtualizarStatus(int codigoVenda, StatusVenda novoStatus)
        {
            var _venda = this._apiContext.Vendas.Where(i => i.Id == codigoVenda).SingleOrDefault();

            if (_venda != null)
            {
                _venda.Status = novoStatus;
                await this._apiContext.SaveChangesAsync();
            }
        }

        public bool VerificarIdExiste(int id)
        {
            return (this._apiContext.Vendas.Where(i => i.Id == id).Count() > 0);
        }

        public VendaModel BuscarPorId(int id)
        {
            var _vendaModel = this._apiContext.Vendas.Where(i => i.Id == id).Include(c=> c.ListaVeiculos).Include(c=> c.Vendedor).SingleOrDefault();

            return (_vendaModel);
        }
    }
}