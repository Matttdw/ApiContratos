using System.Text.Json.Serialization;

namespace ApiContratos.Models
{
    // Modelo que representa um contrato
    public class Contrato
    {
        // Identificador único do contrato
        public int Id { get; set; }

        // Número do contrato
        public string Numero { get; set; } = string.Empty;

        // Nome do cliente associado ao contrato
        public string Cliente { get; set; } = string.Empty;

        // Data de início e vencimento do contrato
        public DateOnly DataInicio { get; set; }
        public DateOnly DataVencimento { get; set; }

        // Indica se o contrato possui renovação automática
        public bool RenovacaoAutomatica { get; set; } = false;

        // Propriedade calculada para obter o vencimento efetivo do contrato
        [JsonIgnore]
        public DateOnly VencimentoEfetivo => GetVencimentoEfetivo();

        // Propriedade calculada para indicar se o contrato está ativo
        public bool Ativo => GetVencimentoEfetivo() >= DateOnly.FromDateTime(DateTime.Today);

        // Descrição opcional do contrato
        public string Descricao { get; set; } = string.Empty;

        // Método que calcula o vencimento efetivo considerando a renovação automática
        public DateOnly GetVencimentoEfetivo()
        {
            var hoje = DateOnly.FromDateTime(DateTime.Today);
            var venc = DataVencimento;

            // Se não houver renovação automática, retorna a data de vencimento original
            if (!RenovacaoAutomatica)
                return venc;

            // Ajusta o vencimento para o próximo ano até que seja no futuro
            while (venc < hoje)
            {
                venc = venc.AddYears(1);
            }
            return venc;
        }
    }
}
