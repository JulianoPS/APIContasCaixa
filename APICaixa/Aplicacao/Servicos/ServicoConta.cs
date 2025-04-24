using APICaixa.Aplicacao.DTOs;
using APICaixa.Dominio.Entidades;
using APICaixa.Dominio.Interfaces;
using Infraestrutura.Dados;

namespace APICaixa.Aplicacao.Servicos
{
    public class ServicoConta
    {
        private readonly IRepositorioConta _repositorioConta;
        private readonly IRepositorioLog _repositorioLog;
        private readonly BDContexto _context;
        public ServicoConta(IRepositorioConta repositorioConta, IRepositorioLog repositorioLog, BDContexto context)
        {
            _repositorioConta = repositorioConta;
            _repositorioLog = repositorioLog;
            _context = context;
        }

        public async Task CriarContaAsync(string nome, string documento)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(documento))
                throw new Exception("O documento é obrigatório.");

            if (await _repositorioConta.ExistePorDocumentoAsync(documento))
                throw new Exception("Já existe conta para este documento");

            var conta = new ContaBancaria { Nome = nome, Documento = documento };
            await _repositorioConta.AdicionarAsync(conta);
        }

        public async Task<IEnumerable<ContaDto>> BuscarContasAsync(string? nome, string? documento)
        {
            var contas = await _repositorioConta.BuscarAsync(nome, documento);

            return contas.Select(c => new ContaDto
            {
                Nome = c.Nome,
                Documento = c.Documento,
                Saldo = c.Saldo,
                Ativa = c.Ativa,
                DataAbertura = c.DataAbertura
            });
        }

        public async Task DesativarContaAsync(string documento, string usuario)
        {
            var conta = await _repositorioConta.ObterPorDocumentoAsync(documento);
            if (conta is null || !conta.Ativa)
                throw new Exception("Conta não encontrada ou já inativa");

            conta.Desativar();
            await _repositorioLog.AdicionarLogDesativacaoAsync(new LogDesativacaoConta
            {
                Documento = documento,
                RealizadoPor = usuario
            });
            await _repositorioConta.AtualizarAsync(conta);
        }

        public async Task RealizarTransferenciaAsync(string documentoOrigem, string documentoDestino, decimal valor)
        {
            if (valor <= 0)
                throw new Exception("Valor da transferência deve ser maior que zero.");

            if (documentoOrigem.Equals(documentoDestino))
                throw new Exception("Documento de Destino igual ao Documento de Origem.");

            var origem = await _repositorioConta.ObterPorDocumentoAsync(documentoOrigem);
            var destino = await _repositorioConta.ObterPorDocumentoAsync(documentoDestino);

            if (origem == null || destino == null || !origem.Ativa || !destino.Ativa)
                throw new Exception("Contas inválidas ou inativas");

            if (origem.Saldo < valor)
                throw new Exception("Saldo insuficiente na conta de origem.");

            using var transacao = await _context.Database.BeginTransactionAsync();
            try
            {
                origem.Debitar(valor);
                destino.Creditar(valor);

                await _repositorioConta.AtualizarAsync(origem);
                await _repositorioConta.AtualizarAsync(destino);

                await _repositorioLog.AdicionarLogTransferenciaAsync(new LogTransferencia
                {
                    DocumentoOrigem = documentoOrigem,
                    DocumentoDestino = documentoDestino,
                    Valor = valor,
                    DataTransferencia = DateTime.UtcNow
                });

                await transacao.CommitAsync();
            }
            catch
            {
                await transacao.RollbackAsync();
                throw;
            }
        }
    }

}
