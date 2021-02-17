using System;
using System.Collections.Generic;
using WebVenda.Enumeradores;

namespace WebVenda.Dto
{
    public sealed class VendaDto
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public int VendedorId { get; set; }
        public VendedorDto Vendedor { get; set; }
        public List<VeiculoDto> ListaVeiculos { get; set; }
        public StatusVenda Status { get; set; }

        public VendaDto()
        {
            DataVenda = DateTime.Now;
            ListaVeiculos = new List<VeiculoDto>();
        }
    }
}