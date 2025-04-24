using APICaixa.Aplicacao.Servicos;
using APICaixa.Testes.Mocks;
using Moq;
using Xunit;

namespace APICaixa.Testes.Servicos
{
    public class ServicoContaTest
    {
        
        [Fact]
        public async Task CriarContaAsync_DeveLancarExcecao_QuandoNomeForNuloOuVazio()
        {
            var mockConta = MockRepositorioConta.ObterRepositorioContaMock();
            var mockLog = MockRepositorioConta.ObterRepositorioLogMock();
            var contexto = MockRepositorioConta.ObterContextoMock();
            var servico = new ServicoConta(mockConta.Object, mockLog.Object, contexto);

            var exception = await Assert.ThrowsAsync<Exception>(() => servico.CriarContaAsync("", "12345678900"));
            Assert.Equal("O nome é obrigatório.", exception.Message);

            exception = await Assert.ThrowsAsync<Exception>(() => servico.CriarContaAsync(null, "12345678900"));
            Assert.Equal("O nome é obrigatório.", exception.Message);
        }

        [Fact]
        public async Task CriarContaAsync_DeveLancarExcecao_QuandoDocumentoForNuloOuVazio()
        {
            var mockConta = MockRepositorioConta.ObterRepositorioContaMock();
            var mockLog = MockRepositorioConta.ObterRepositorioLogMock();
            var contexto = MockRepositorioConta.ObterContextoMock();
            var servico = new ServicoConta(mockConta.Object, mockLog.Object, contexto);

            var exception = await Assert.ThrowsAsync<Exception>(() => servico.CriarContaAsync("Maria Teste", ""));
            Assert.Equal("O documento é obrigatório.", exception.Message);

            exception = await Assert.ThrowsAsync<Exception>(() => servico.CriarContaAsync("Maria Teste", null));
            Assert.Equal("O documento é obrigatório.", exception.Message);
        }

        [Fact]
        public async Task CriarContaAsync_DeveLancarExcecao_QuandoDocumentoJaExistir()
        {
            var mockConta = MockRepositorioConta.ObterRepositorioContaMock();
            var mockLog = MockRepositorioConta.ObterRepositorioLogMock();
            var contexto = MockRepositorioConta.ObterContextoMock();
            var servico = new ServicoConta(mockConta.Object, mockLog.Object, contexto);

            mockConta.Setup(r => r.ExistePorDocumentoAsync("12345678900"))
                     .ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<Exception>(() => servico.CriarContaAsync("Maria Teste", "12345678900"));
            Assert.Equal("Já existe conta para este documento", exception.Message);
        }

        [Fact]
        public async Task DesativarContaAsync_DeveLancarExcecao_QuandoContaNaoExistir()
        {
            var mockConta = MockRepositorioConta.ObterRepositorioContaMock();
            var mockLog = MockRepositorioConta.ObterRepositorioLogMock();
            var contexto = MockRepositorioConta.ObterContextoMock();
            var servico = new ServicoConta(mockConta.Object, mockLog.Object, contexto);

            mockConta.Setup(r => r.ObterPorDocumentoAsync("12345678900"))
                     .ReturnsAsync((APICaixa.Dominio.Entidades.ContaBancaria)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => servico.DesativarContaAsync("12345678900", "usuario"));
            Assert.Equal("Conta não encontrada ou já inativa", exception.Message);
        }

        [Fact]
        public async Task DesativarContaAsync_DeveDesativarConta_QuandoContaExistirEEstiverAtiva()
        {
            var mockConta = MockRepositorioConta.ObterRepositorioContaMock();
            var mockLog = MockRepositorioConta.ObterRepositorioLogMock();
            var contexto = MockRepositorioConta.ObterContextoMock();
            var servico = new ServicoConta(mockConta.Object, mockLog.Object, contexto);

            var contaAtiva = new APICaixa.Dominio.Entidades.ContaBancaria { Documento = "12345678900", Ativa = true };
            mockConta.Setup(r => r.ObterPorDocumentoAsync("12345678900"))
                     .ReturnsAsync(contaAtiva);

            await servico.DesativarContaAsync("12345678900", "usuario");

            mockConta.Verify(r => r.AtualizarAsync(contaAtiva), Times.Once, "O método AtualizarAsync não foi chamado para desativar a conta.");
            mockLog.Verify(r => r.AdicionarLogDesativacaoAsync(It.IsAny<APICaixa.Dominio.Entidades.LogDesativacaoConta>()), Times.Once, "O log de desativação não foi registrado.");
        }

        [Fact]
        public async Task RealizarTransferenciaAsync_DeveLancarExcecao_QuandoValorForMenorOuIgualAZero()
        {
            var mockConta = MockRepositorioConta.ObterRepositorioContaMock();
            var mockLog = MockRepositorioConta.ObterRepositorioLogMock();
            var contexto = MockRepositorioConta.ObterContextoMock();
            var servico = new ServicoConta(mockConta.Object, mockLog.Object, contexto);

            var exception = await Assert.ThrowsAsync<Exception>(() => servico.RealizarTransferenciaAsync("12345678900", "22345678900", 0));
            Assert.Equal("Valor da transferência deve ser maior que zero.", exception.Message);

            exception = await Assert.ThrowsAsync<Exception>(() => servico.RealizarTransferenciaAsync("12345678900", "22345678900", -10));
            Assert.Equal("Valor da transferência deve ser maior que zero.", exception.Message);
        }

        [Fact]
        public async Task RealizarTransferenciaAsync_DeveLancarExcecao_QuandoDocumentoOrigemForIgualADestino()
        {
            var mockConta = MockRepositorioConta.ObterRepositorioContaMock();
            var mockLog = MockRepositorioConta.ObterRepositorioLogMock();
            var contexto = MockRepositorioConta.ObterContextoMock();
            var servico = new ServicoConta(mockConta.Object, mockLog.Object, contexto);

            var exception = await Assert.ThrowsAsync<Exception>(() => servico.RealizarTransferenciaAsync("12345678900", "12345678900", 100));
            Assert.Equal("Documento de Destino igual ao Documento de Origem.", exception.Message);
        }

        [Fact]
        public async Task RealizarTransferenciaAsync_DeveLancarExcecao_QuandoContasNaoExistiremOuEstiveremInativas()
        {
            var mockConta = MockRepositorioConta.ObterRepositorioContaMock();
            var mockLog = MockRepositorioConta.ObterRepositorioLogMock();
            var contexto = MockRepositorioConta.ObterContextoMock();
            var servico = new ServicoConta(mockConta.Object, mockLog.Object, contexto);

            mockConta.Setup(r => r.ObterPorDocumentoAsync("12345678900"))
                     .ReturnsAsync((APICaixa.Dominio.Entidades.ContaBancaria)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => servico.RealizarTransferenciaAsync("12345678900", "22345678900", 100));
            Assert.Equal("Contas inválidas ou inativas", exception.Message);
        }

        [Fact]
        public async Task RealizarTransferenciaAsync_DeveLancarExcecao_QuandoSaldoDeOrigemForInsuficiente()
        {
            var mockConta = MockRepositorioConta.ObterRepositorioContaMock();
            var mockLog = MockRepositorioConta.ObterRepositorioLogMock();
            var contexto = MockRepositorioConta.ObterContextoMock();
            var servico = new ServicoConta(mockConta.Object, mockLog.Object, contexto);

            var contaOrigem = new APICaixa.Dominio.Entidades.ContaBancaria { Documento = "12345678900" };
            var contaDestino = new APICaixa.Dominio.Entidades.ContaBancaria { Documento = "22345678900" };

            mockConta.Setup(r => r.ObterPorDocumentoAsync("12345678900")).ReturnsAsync(contaOrigem);
            mockConta.Setup(r => r.ObterPorDocumentoAsync("22345678900")).ReturnsAsync(contaDestino);

            var exception = await Assert.ThrowsAsync<Exception>(() => servico.RealizarTransferenciaAsync("12345678900", "22345678900", 1001));
            Assert.Equal("Saldo insuficiente na conta de origem.", exception.Message);
        }
        
    }
}
