namespace APICaixa.Dominio.Entidades
{
    public class LogTransferencia
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DocumentoOrigem { get; set; } = string.Empty;
        public string DocumentoDestino { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataTransferencia { get; set; } = DateTime.UtcNow;
    }
}
