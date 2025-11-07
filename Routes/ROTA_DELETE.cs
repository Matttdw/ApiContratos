using ApiContratos.Models;

namespace ApiContratos.Routes
{
    public static class RotasDELETE
    {
        public static void Map(WebApplication app, List<Contrato> contratos)
        {
            app.MapDelete("/contratos/{id}", (int id) =>
            {
                var contrato = contratos.FirstOrDefault(c => c.Id == id);

                if (contrato == null)
                    return Results.NotFound("Contrato não encontrado.");

                contratos.Remove(contrato);

                return Results.Ok($"Contrato {id} deletado com sucesso.");
            });
        }
    }
}
