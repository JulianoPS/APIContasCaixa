using APICaixa.Dominio.Entidades;

namespace APICaixa.Dominio.Interfaces
{
    public interface IRepositorioConta
    {
        Task<bool> ExistePorDocumentoAsync(string documento);
        Task AdicionarAsync(ContaBancaria conta);
        Task AtualizarAsync(ContaBancaria conta);
        Task<ContaBancaria?> ObterPorDocumentoAsync(string documento);
        Task<IEnumerable<ContaBancaria>> BuscarAsync(string? nome, string? documento);
    }
}
