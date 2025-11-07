using System.Text.Json.Serialization;

namespace ApiContratos.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;

        public DateOnly DataInicio { get; set; }
        public DateOnly DataVencimento { get; set; }

        public bool RenovacaoAutomatica { get; set; } = false;

        [JsonIgnore]
        public DateOnly VencimentoEfetivo => GetVencimentoEfetivo();

        public bool Ativo => GetVencimentoEfetivo() >= DateOnly.FromDateTime(DateTime.Today);

        public string Descricao { get; set; } = string.Empty;

        public DateOnly GetVencimentoEfetivo()
        {
            var hoje = DateOnly.FromDateTime(DateTime.Today);
            var venc = DataVencimento;

            if (!RenovacaoAutomatica)
                return venc;

            while (venc < hoje)
            {
                venc = venc.AddYears(1);
            }
            return venc;
        }
    }
}
