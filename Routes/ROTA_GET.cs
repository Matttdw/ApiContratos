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
                var contratos = await db.Contratos.AsNoTracking().ToListAsync();
                return Results.Ok(contratos);
            }).WithName("GetContratos");

            app.MapGet("/api/contratos/{id:int}", async (int id, AppDbContext db) =>
            {
                var contrato = await db.Contratos.FindAsync(id);
                if (contrato == null) return Results.NotFound(new { message = "Contrato não encontrado." });

                return Results.Ok(contrato);
            }).WithName("GetContratoById");
        }
    }
}
