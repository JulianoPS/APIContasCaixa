using APICaixa.Dominio.Interfaces;
using Infraestrutura.Dados;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICaixa.Testes.Mocks
{
    public static class MockRepositorioConta
    {
        public static BDContexto ObterContextoMock()
        {
            var options = new DbContextOptionsBuilder<BDContexto>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // banco em memória para teste
                .Options;

            return new BDContexto(options);
        }
        public static Mock<IRepositorioConta> ObterRepositorioContaMock()
        {
            var mock = new Mock<IRepositorioConta>();
            mock.Setup(r => r.ExistePorDocumentoAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            mock.Setup(r => r.AdicionarAsync(It.IsAny<APICaixa.Dominio.Entidades.ContaBancaria>()))
                .Returns(Task.CompletedTask);

            return mock;
        }

        public static Mock<IRepositorioLog> ObterRepositorioLogMock()
        {
            var mock = new Mock<IRepositorioLog>();
            mock.Setup(r => r.AdicionarLogDesativacaoAsync(It.IsAny<APICaixa.Dominio.Entidades.LogDesativacaoConta>()))
                .Returns(Task.CompletedTask);

            mock.Setup(r => r.AdicionarLogTransferenciaAsync(It.IsAny<APICaixa.Dominio.Entidades.LogTransferencia>()))
                .Returns(Task.CompletedTask);

            return mock;
        }
    }
}
