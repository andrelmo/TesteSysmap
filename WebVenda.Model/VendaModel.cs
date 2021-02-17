using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebVenda.Enumeradores;

namespace WebVenda.Model
{
    public class VendaModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public int VendedorId { get; set; }
        public virtual VendedorModel Vendedor { get; set; }
        public virtual List<VeiculoModel> ListaVeiculos { get; set; }
        public StatusVenda Status { get; set; }

        public VendaModel()
        {
            DataVenda = DateTime.Now;
            ListaVeiculos = new List<VeiculoModel>();
        }
    }
}