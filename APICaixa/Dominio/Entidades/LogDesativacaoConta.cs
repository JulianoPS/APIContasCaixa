namespace APICaixa.Dominio.Entidades
{
    public class LogDesativacaoConta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Documento { get; set; } = string.Empty;
        public string RealizadoPor { get; set; } = "sistema";
        public DateTime DataHoraDesativacao { get; set; } = DateTime.UtcNow;
    }
}
