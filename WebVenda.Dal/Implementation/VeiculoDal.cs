using System.Linq;
using WebVenda.Dal.Interface;
using WebVenda.Model;

namespace WebVenda.Dal.Implementation
{
    public sealed class VeiculoDal : IVeiculoDal
    {
        private readonly ApiContext _apiContext;

        public VeiculoDal(ApiContext apiContext)
        {
            this._apiContext = apiContext;
        }

        public bool VerificarCodigoExiste(int codigo)
        {
            return (this._apiContext.Veiculos.Where(i => i.Codigo == codigo).Count() > 0);
        }

        public VeiculoModel BuscarPorCodigo(int codigo)
        {
            return (this._apiContext.Veiculos.Where(i => i.Codigo == codigo).SingleOrDefault());
        }
    }
}