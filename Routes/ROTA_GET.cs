using ApiContratos.Models;
using ApiContratos.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContratos.Routes
{
    public static class RotasGET
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/contratos", async (AppDbContext db) =>
            {
                // apply potential renewals by computing effective vencimento (updates in-memory objects if desired)
                var contratos = await db.Contratos.AsNoTracking().ToListAsync();
                // We will return the computed Ativo based on vencimento efetivo (in model)
                return Results.Ok(contratos);
            }).WithName("GetContratos");

            app.MapGet("/api/contratos/{id:int}", async (int id, AppDbContext db) =>
            {
                var contrato = await db.Contratos.FindAsync(id);
                if (contrato == null) return Results.NotFound(new { message = "Contrato não encontrado." });

                // If RenovacaoAutomatica and vencimento passed, we won't mutate DB here (To keep seed stable) -
                // Ativo property uses computed vencimento when serialized.
                return Results.Ok(contrato);
            }).WithName("GetContratoById");
        }
    }
}
