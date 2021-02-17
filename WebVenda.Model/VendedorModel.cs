using System.ComponentModel.DataAnnotations;

namespace WebVenda.Model
{
    public sealed class VendedorModel
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}
