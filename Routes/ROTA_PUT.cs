using ApiContratos.Data;
using ApiContratos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiContratos.Routes
{
    // Classe estática responsável pelas rotas PUT da API
    public static class RotasPUT
    {
        // Método que adiciona as rotas PUT à aplicação
        public static void Map(WebApplication app)
        {
            // Rota para atualizar um contrato existente
            app.MapPut("/api/contratos/{id:int}", async (int id, Contrato dados, AppDbContext db) =>
            {
                // Busca o contrato pelo ID
                var contrato = await db.Contratos.FirstOrDefaultAsync(c => c.Id == id);

                // Se não encontrado, retorna 404
                if (contrato == null)
                    return Results.NotFound(new { message = "Contrato não encontrado." });

                // Atualiza os campos
                contrato.Numero = dados.Numero;
                contrato.Cliente = dados.Cliente;
                contrato.DataInicio = dados.DataInicio;
                contrato.DataVencimento = dados.DataVencimento;
                contrato.RenovacaoAutomatica = dados.RenovacaoAutomatica;
                contrato.Descricao = dados.Descricao;

                // Salva as alterações no banco de dados
                await db.SaveChangesAsync();

                // Retorna o contrato atualizado
                return Results.Ok(contrato);
            })
            .WithName("AtualizarContrato");
        }
    }
}
