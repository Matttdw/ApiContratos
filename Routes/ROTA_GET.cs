using ApiContratos.Models;

namespace ApiContratos.Routes
{
    public static class RotasGET
    {
        public static void Map(WebApplication app, List<Contrato> contratos)
        {
            app.MapGet("/contratos", () => Results.Ok(contratos));

            app.MapGet("/contratos/{id}", (int id) =>
            {
                var contrato = contratos.FirstOrDefault(c => c.Id == id);

                if (contrato == null)
                    return Results.NotFound("Contrato não encontrado.");

                return Results.Ok(contrato);
            });
        }
    }
}
