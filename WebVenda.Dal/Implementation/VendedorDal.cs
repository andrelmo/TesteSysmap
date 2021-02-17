using System.Linq;
using WebVenda.Dal.Interface;
using WebVenda.Model;

namespace WebVenda.Dal.Implementation
{
    public sealed class VendedorDal : IVendedorDal
    {
        private readonly ApiContext _apiContext;

        public VendedorDal(ApiContext apiContext)
        {
            this._apiContext = apiContext;
        }

        public bool VerificarCodigoExiste(int codigo)
        {
            return (this._apiContext.Vendedores.Where(i => i.Codigo == codigo).Count() > 0);
        }

        public VendedorModel BuscarPorCodigo(int codigo)
        {
            return (this._apiContext.Vendedores.Where(i => i.Codigo == codigo).SingleOrDefault());
        }
    }
}
