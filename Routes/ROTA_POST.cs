using ApiContratos.Models;
using ApiContratos.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContratos.Routes
{
    // Classe estática responsável pelas rotas POST da API
    public static class RotasPOST
    {
        // Método que adiciona as rotas POST à aplicação
        public static void Map(WebApplication app)
        {
            // Rota para criar um novo contrato
            app.MapPost("/api/contratos", async (Contrato novoContrato, AppDbContext db) =>
            {
                // Validação simples dos dados recebidos
                if (string.IsNullOrWhiteSpace(novoContrato.Numero) || string.IsNullOrWhiteSpace(novoContrato.Cliente))
                    return Results.BadRequest(new { message = "Numero e Cliente são obrigatórios." });

                // Garante que o ID seja zero para evitar conflitos
                novoContrato.Id = 0;

                // Adiciona o novo contrato ao banco de dados
                db.Contratos.Add(novoContrato);
                await db.SaveChangesAsync();

                // Retorna a resposta com o contrato criado
                return Results.Created($"/api/contratos/{novoContrato.Id}", novoContrato);
            }).WithName("CreateContrato");
        }
    }
}
