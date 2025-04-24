using APICaixa.Dominio.Entidades;
using APICaixa.Dominio.Interfaces;
using Infraestrutura.Dados;

namespace APICaixa.Infraestrutura.Repositorios
{
    public class RepositorioLog : IRepositorioLog
    {
        private readonly BDContexto _context;

        public RepositorioLog(BDContexto context)
        {
            _context = context;
        }

        public async Task AdicionarLogDesativacaoAsync(LogDesativacaoConta log)
        {
            _context.LogsDesativacaoConta.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task AdicionarLogTransferenciaAsync(LogTransferencia log)
        {
            _context.LogsTransferencia.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
