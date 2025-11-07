namespace ApiContratos.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public DateOnly DataInicio { get; set; }
        public DateOnly DataVencimento { get; set; }
        public bool Ativo => DataVencimento >= DateOnly.FromDateTime(DateTime.Today);
        public string Descricao { get; set; } = string.Empty;

        public bool RenovacaoAutomatica { get; set; }

    }
}