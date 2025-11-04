namespace ApiContratos.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Ativo { get; set; }
        public string Descricao { get; set; } = string.Empty;

    }
}