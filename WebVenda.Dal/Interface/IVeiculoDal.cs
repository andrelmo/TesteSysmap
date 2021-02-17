using WebVenda.Model;

namespace WebVenda.Dal.Interface
{
    public interface IVeiculoDal
    {
        bool VerificarCodigoExiste(int codigo);
        VeiculoModel BuscarPorCodigo(int codigo);
    }
}