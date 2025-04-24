using APICaixa.Aplicacao.Servicos;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APICaixa.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly ServicoConta _servicoConta;
        public ContaController(ServicoConta servicoConta)
        {
            _servicoConta = servicoConta;
        }

        /// <summary>
        /// Cria uma nova conta bancária.
        /// </summary>
        /// <param name="requisicao">Dados necessários para criar a conta.</param>
        /// <returns>Retorna 201 em caso de sucesso, ou 400 se houver erro.</returns>
        /// <response code="201">Conta criada com sucesso.</response>
        /// <response code="400">Erro na criação da conta.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarConta([FromBody] CriarContaRequisicao requisicao)
        {
            try
            {
                await _servicoConta.CriarContaAsync(requisicao.Nome, requisicao.Documento);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        /// <summary>
        /// Lista contas com filtro opcional por nome ou documento.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? nome, [FromQuery] string? documento)
        {
            var contas = await _servicoConta.BuscarContasAsync(nome, documento);
            return Ok(contas);
        }

        /// <summary>
        /// Desativa uma conta pelo documento e registra o usuário responsável.
        /// </summary>
        [HttpPatch("desativar")]
        public async Task<IActionResult> DesativarConta([FromBody] DesativarContaRequest request)
        {
            try
            {
                await _servicoConta.DesativarContaAsync(request.Documento, request.Usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        public record DesativarContaRequest(string Documento, string Usuario);

        /// <summary>
        /// Realiza uma transferência entre contas bancárias.
        /// </summary>
        /// <param name="request">Dados da transferência: origem, destino e valor.</param>
        /// <response code="200">Transferência realizada com sucesso.</response>
        /// <response code="400">Erro na transferência.</response>
        [HttpPost("transferir")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Transferir([FromBody] TransferenciaRequest request)
        {
            try
            {
                await _servicoConta.RealizarTransferenciaAsync(
                    request.DocumentoOrigem, request.DocumentoDestino, request.Valor);
                return Ok(new { mensagem = "Transferência realizada com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        public record TransferenciaRequest(string DocumentoOrigem, string DocumentoDestino, decimal Valor);

    }

    /// <summary>
    /// Requisição para criação de conta.
    /// </summary>
    public class CriarContaRequisicao
    {
        /// <example>João da Silva</example>
        [Required(ErrorMessage = "O nome é obrigatório.")] 
        public string Nome { get; set; } = string.Empty;

        /// <example>12345678900</example>
        [Required(ErrorMessage = "O documento é obrigatório.")] 
        public string Documento { get; set; } = string.Empty;
    }




}
