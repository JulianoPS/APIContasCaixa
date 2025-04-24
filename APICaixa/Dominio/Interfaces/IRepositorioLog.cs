using APICaixa.Dominio.Entidades;

namespace APICaixa.Dominio.Interfaces
{
    public interface IRepositorioLog
    {
        Task AdicionarLogTransferenciaAsync(LogTransferencia log);
        Task AdicionarLogDesativacaoAsync(LogDesativacaoConta log);
    }
}
