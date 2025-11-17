using ApiContratos.Data;
using ApiContratos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiContratos.Routes
{
    public static class RotasPUT
    {
        public static void Map(WebApplication app)
        {
            app.MapPut("/api/contratos/{id:int}", async (int id, Contrato dados, AppDbContext db) =>
            {
                var contrato = await db.Contratos.FirstOrDefaultAsync(c => c.Id == id);

                if (contrato == null)
                    return Results.NotFound(new { message = "Contrato não encontrado." });

                // Atualiza os campos
                contrato.Numero = dados.Numero;
                contrato.Cliente = dados.Cliente;
                contrato.DataInicio = dados.DataInicio;
                contrato.DataVencimento = dados.DataVencimento;
                contrato.RenovacaoAutomatica = dados.RenovacaoAutomatica;
                contrato.Descricao = dados.Descricao;

                await db.SaveChangesAsync();

                return Results.Ok(contrato);
            })
            .WithName("AtualizarContrato");
        }
    }
}
