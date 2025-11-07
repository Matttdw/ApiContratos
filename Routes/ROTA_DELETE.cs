using ApiContratos.Data;

namespace ApiContratos.Routes
{
    // Classe estática responsável pelas rotas DELETE da API
    public static class RotasDELETE
    {
        // Método que adiciona as rotas DELETE à aplicação
        public static void Map(WebApplication app)
        {
            // Rota para deletar um contrato pelo ID
            app.MapDelete("/api/contratos/{id:int}", async (int id, AppDbContext db) =>
            {
                // Busca o contrato pelo ID
                var contrato = await db.Contratos.FindAsync(id);
                if (contrato == null) return Results.NotFound(new { message = "Contrato não encontrado." });

                // Remove o contrato do banco de dados
                db.Contratos.Remove(contrato);
                await db.SaveChangesAsync();

                // Retorna a confirmação da remoção
                return Results.Ok(new { message = $"Contrato {id} removido." });
            }).WithName("DeleteContrato");
        }
    }
}
