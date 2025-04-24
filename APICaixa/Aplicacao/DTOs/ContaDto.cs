namespace APICaixa.Aplicacao.DTOs
{
    public class ContaDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public bool Ativa { get; set; }
        public DateTime DataAbertura { get; set; } 
    }
}
