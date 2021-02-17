using System.ComponentModel.DataAnnotations;

namespace WebVenda.Model
{
    public sealed class VeiculoModel
    {
        [Key]
        public int Codigo { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
    }
}