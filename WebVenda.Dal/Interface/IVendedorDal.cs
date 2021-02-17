using WebVenda.Model;

namespace WebVenda.Dal.Interface
{
    public interface IVendedorDal
    {
        bool VerificarCodigoExiste(int codigo);
        VendedorModel BuscarPorCodigo(int codigo);
    }
}