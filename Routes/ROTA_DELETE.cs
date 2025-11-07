using ApiContratos.Data;

namespace ApiContratos.Routes
{
    public static class RotasDELETE
    {
        public static void Map(WebApplication app)
        {
            app.MapDelete("/api/contratos/{id:int}", async (int id, AppDbContext db) =>
            {
                var contrato = await db.Contratos.FindAsync(id);
                if (contrato == null) return Results.NotFound(new { message = "Contrato não encontrado." });

                db.Contratos.Remove(contrato);
                await db.SaveChangesAsync();
                return Results.Ok(new { message = $"Contrato {id} removido." });
            }).WithName("DeleteContrato");
        }
    }
}
