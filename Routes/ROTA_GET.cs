using ApiContratos.Models;

namespace ApiContratos.Routes
{
    public static class RotasGET
    {
        public static void Map(WebApplication app, List<Contrato> contratos)
        {
            app.MapGet("/contratos", async () =>
            {
                await Task.Delay(10);
                return Results.Ok(contratos);
            });

            app.MapGet("/contratos/{id}", async (int id) =>
            {
                await Task.Delay(10);

                var contrato = contratos.FirstOrDefault(c => c.Id == id);
                return contrato == null
                    ? Results.NotFound("Contrato não encontrado.")
                    : Results.Ok(contrato);
            });
        }
    }

}
