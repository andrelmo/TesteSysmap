using System.Threading.Tasks;
using WebVenda.Enumeradores;
using WebVenda.Model;

namespace WebVenda.Dal.Interface
{
    public interface IVendaDal
    {
        Task<RegistrarVendaModel> Inserir(RegistrarVendaModel venda);
        Task AtualizarStatus(int codigoVenda, StatusVenda novoStatus);
        VendaModel BuscarPorId(int id);
        bool VerificarIdExiste(int id);
    }
}
