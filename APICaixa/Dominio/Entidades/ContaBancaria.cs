namespace APICaixa.Dominio.Entidades
{
    public class ContaBancaria
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public decimal Saldo { get; private set; } = 1000;
        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;
        public bool Ativa { get; set; } = true;

        public void Creditar(decimal valor) => Saldo += valor;
        public void Debitar(decimal valor)
        {
            if (valor > Saldo)
                throw new InvalidOperationException("Saldo insuficiente");
            Saldo -= valor;
        }
        public void Desativar() => Ativa = false;
    }
}
