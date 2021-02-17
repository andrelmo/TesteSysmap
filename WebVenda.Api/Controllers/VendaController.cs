using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebVenda.Dal.Interface;
using WebVenda.Dto;
using WebVenda.Enumeradores;
using WebVenda.Model;

namespace WebVenda.Api.Controllers
{
    [ApiController]
    public sealed class VendaController : ControllerBase
    {
        private const string STATUS_CANCELADA = @"Cancelada";
        private const string STATUS_CONFIRMACAO_PAGAMENTO = @"Confirmação de Pagamento";
        private const string STATUS_EM_TRANSPORTE = @"Em Transporte";
        private const string STATUS_ENTREGUE = @"Entregue";
        private const string STATUS_PAGAMENTO_APROVADO = @"Pagamento Aprovado";

        private readonly IVendaDal _vendaDal;
        private readonly IVendedorDal _vendedorDal;
        private readonly IMapper _mapper;
        private readonly IVeiculoDal _veiculoDal;

        public VendaController(IVendaDal vendaDal, IMapper mapper, IVendedorDal vendedorDal, IVeiculoDal veiculoDal)
        {
            this._vendaDal = vendaDal;
            this._mapper = mapper;
            this._vendedorDal = vendedorDal;
            this._veiculoDal = veiculoDal;
        }

        [HttpGet]
        [Route("Venda/Atualizar/{id}/{novoStatus}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AtualizarStatus([FromRoute]int id, StatusVenda novoStatus)
        {
            try
            {
                if (id <= 0)
                    return (BadRequest("O identificador da venda deve ser informado!"));

                if (!this._vendaDal.VerificarIdExiste(id))
                    return (NotFound($"O identificador da venda {id} informado não foi encontrado!"));

                var _vendaModel = this._vendaDal.BuscarPorId(id);

                if (!this.IsAtualizacaoStatusValida(_vendaModel, novoStatus))
                    return (BadRequest($"A transição do status {GetDescricaoStatus(_vendaModel.Status)} para o status {GetDescricaoStatus(novoStatus)} não é válida!"));

                await this._vendaDal.AtualizarStatus(id, novoStatus);

                return (Ok());
            }
            catch (Exception Ex)
            {
                return (BadRequest($"Ocorreu o seguinte erro no método AtualizarStatus.Erro: {Ex.Message}"));
            }
        }

        [HttpGet]
        [Route("Venda/BuscarPorId/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(VendaDto), StatusCodes.Status200OK)]
        [Produces(typeof(VendaDto))]
        public IActionResult BuscarPorId([FromRoute]int id)
        {
            try
            {
                if (id <= 0)
                    return (BadRequest("O identificador da venda deve ser informado!"));

                if (!this._vendaDal.VerificarIdExiste(id))
                    return (NotFound($"O identificador da venda {id} informado não foi encontrado!"));

                var _vendaModel = this._vendaDal.BuscarPorId(id);

                return (Ok(_mapper.Map<VendaDto>(_vendaModel)));
            }
            catch (Exception Ex)
            {
                return (BadRequest($"Ocorreu o seguinte erro no método BuscarPorId.Erro: {Ex.Message}"));
            }
        }

        [HttpPost]
        [Route("Venda/GravarVendaVeiculo")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(RegistrarVendaDto), StatusCodes.Status200OK)]
        [Produces(typeof(RegistrarVendaDto))]
        public async Task<IActionResult> GravarVendaVeiculo([FromBody] RegistrarVendaDto venda)
        {
            try
            {
                if (venda.CodigoVendedor <= 0)
                    return (BadRequest("O código do vendedor deve ser informado!"));

                if (!_vendedorDal.VerificarCodigoExiste(venda.CodigoVendedor))
                    return (BadRequest($"O código do vendedor {venda.CodigoVendedor} informado não foi encontrado!"));

                if (venda.ListaVeiculos.Count == 0)
                    return (BadRequest("A venda deve ter pelo menos um veículo"));

                foreach (var _veiculo in venda.ListaVeiculos)
                {
                    if (!this._veiculoDal.VerificarCodigoExiste(_veiculo))
                        return (BadRequest($"O código do veículo {_veiculo} informado não foi encontrado!"));
                }

                var _registrarVendaModel = _mapper.Map<RegistrarVendaModel>(venda);

                _registrarVendaModel.Status = StatusVenda.ConfirmacaoPagamento;
                _registrarVendaModel = await this._vendaDal.Inserir(_registrarVendaModel);

                return (Ok(_mapper.Map<RegistrarVendaDto>(_registrarVendaModel)));
            }
            catch (Exception Ex)
            {
                return (BadRequest($"Ocorreu o seguinte erro no método GravarVendaVeiculo.Erro: {Ex.Message}"));
            }
        }

        private bool IsAtualizacaoStatusValida(VendaModel venda, StatusVenda novoStatus)
        {
            if (venda.Status == StatusVenda.ConfirmacaoPagamento && novoStatus == StatusVenda.PagamentoAprovado)
                return (true);
            else if (venda.Status == StatusVenda.ConfirmacaoPagamento && novoStatus == StatusVenda.Cancelada)
                return (true);
            else if (venda.Status == StatusVenda.PagamentoAprovado && novoStatus == StatusVenda.EmTransporte)
                return (true);
            else if (venda.Status == StatusVenda.PagamentoAprovado && novoStatus == StatusVenda.Cancelada)
                return (true);
            else if (venda.Status == StatusVenda.EmTransporte && novoStatus == StatusVenda.Entregue)
                return (true);

            return (false);
        }

        private string GetDescricaoStatus(StatusVenda status)
        {
            if (status == StatusVenda.Cancelada)
                return (STATUS_CANCELADA);
            else if (status == StatusVenda.ConfirmacaoPagamento)
                return (STATUS_CONFIRMACAO_PAGAMENTO);
            else if (status == StatusVenda.EmTransporte)
                return (STATUS_EM_TRANSPORTE);
            else if (status == StatusVenda.Entregue)
                return (STATUS_ENTREGUE);
            else
                return (STATUS_PAGAMENTO_APROVADO);
        }
    }
}