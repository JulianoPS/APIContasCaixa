using APICaixa.Dominio.Entidades;
using APICaixa.Dominio.Interfaces;
using Infraestrutura.Dados;
using Microsoft.EntityFrameworkCore;

namespace APICaixa.Infraestrutura.Repositorios
{
    public class RepositorioConta : IRepositorioConta
    {
        private readonly BDContexto _context;

        public RepositorioConta(BDContexto context)
        {
            _context = context;
        }

        public async Task<bool> ExistePorDocumentoAsync(string documento)
        {
            return await _context.Contas.AnyAsync(c => c.Documento == documento);
        }

        public async Task AdicionarAsync(ContaBancaria conta)
        {
            await _context.Contas.AddAsync(conta);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ContaBancaria conta)
        {
            _context.Contas.Update(conta);
            await _context.SaveChangesAsync();
        }

        public async Task<ContaBancaria?> ObterPorDocumentoAsync(string documento)
        {
            return await _context.Contas
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Documento == documento);
        }

        public async Task<IEnumerable<ContaBancaria>> BuscarAsync(string? nome, string? documento)
        {
            var query = _context.Contas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            if (!string.IsNullOrWhiteSpace(documento))
                query = query.Where(c => c.Documento == documento);

            return await query.ToListAsync();
        }
    }
}
