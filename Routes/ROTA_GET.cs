using ApiContratos.Models;
using ApiContratos.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContratos.Routes
{
    // Classe estática responsável pelas rotas GET da API
    public static class RotasGET
    {
        // Método que adiciona as rotas GET à aplicação
        public static void Map(WebApplication app)

        {
            // Rota para obter todos os contratos
            app.MapGet("/api/contratos", async (AppDbContext db) =>
            {
                // Usa AsNoTracking para melhorar a performance em consultas somente leitura
                var contratos = await db.Contratos.AsNoTracking().ToListAsync();
                
                return Results.Ok(contratos);
            }).WithName("GetContratos");

            // Rota para obter um contrato específico pelo ID
            app.MapGet("/api/contratos/{id:int}", async (int id, AppDbContext db) =>
            {
                var contrato = await db.Contratos.FindAsync(id);
                if (contrato == null) return Results.NotFound(new { message = "Contrato não encontrado." });

                return Results.Ok(contrato);
            }).WithName("GetContratoById");
        }
    }
}
