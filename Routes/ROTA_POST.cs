using ApiContratos.Models;

namespace ApiContratos.Routes
{
    public static class RotasPOST
{
    public static void Map(WebApplication app, List<Contrato> contratos)
    {
        app.MapPost("/contratos", async (Contrato novoContrato) =>
        {
            await Task.Delay(10);

            novoContrato.Id = contratos.Count == 0 
                ? 1 
                : contratos.Max(c => c.Id) + 1;

            contratos.Add(novoContrato);

            return Results.Created($"/contratos/{novoContrato.Id}", novoContrato);
        });
    }
}

}
