using ApiContratos.Models;
using ApiContratos.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContratos.Routes
{
    public static class RotasPOST
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/api/contratos", async (Contrato novoContrato, AppDbContext db) =>
            {
                // Basic validation
                if (string.IsNullOrWhiteSpace(novoContrato.Numero) || string.IsNullOrWhiteSpace(novoContrato.Cliente))
                    return Results.BadRequest(new { message = "Numero e Cliente são obrigatórios." });

                // Assign Id will be handled by EF (auto incremental). Ensure Id is 0
                novoContrato.Id = 0;

                // If the client sent a past DataVencimento but marked renovacao, the Ativo computed will reflect that.
                db.Contratos.Add(novoContrato);
                await db.SaveChangesAsync();

                return Results.Created($"/api/contratos/{novoContrato.Id}", novoContrato);
            }).WithName("CreateContrato");
        }
    }
}
