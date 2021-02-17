using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebVenda.Enumeradores;

namespace WebVenda.Model
{
    public sealed class RegistrarVendaModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public int CodigoVendedor { get; set; }
        public List<int> ListaVeiculos { get; set; }
        public StatusVenda Status { get; set; }

        public RegistrarVendaModel()
        {
            DataVenda = DateTime.Now;
            ListaVeiculos = new List<int>();
            Status = StatusVenda.ConfirmacaoPagamento;
        }
    }
}