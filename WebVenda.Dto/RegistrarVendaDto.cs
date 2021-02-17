using System;
using System.Collections.Generic;
using WebVenda.Enumeradores;

namespace WebVenda.Dto
{
    public sealed class RegistrarVendaDto
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public int CodigoVendedor { get; set; }
        public List<int> ListaVeiculos { get; set; }
        public StatusVenda Status { get; set; }

        public RegistrarVendaDto()
        {
            DataVenda = DateTime.Now;
            ListaVeiculos = new List<int>();
            Status = StatusVenda.ConfirmacaoPagamento;
        }
    }
}