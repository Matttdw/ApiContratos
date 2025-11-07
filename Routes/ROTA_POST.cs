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
                if (string.IsNullOrWhiteSpace(novoContrato.Numero) || string.IsNullOrWhiteSpace(novoContrato.Cliente))
                    return Results.BadRequest(new { message = "Numero e Cliente são obrigatórios." });

                novoContrato.Id = 0;

                db.Contratos.Add(novoContrato);
                await db.SaveChangesAsync();

                return Results.Created($"/api/contratos/{novoContrato.Id}", novoContrato);
            }).WithName("CreateContrato");
        }
    }
}
