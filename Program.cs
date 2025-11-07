using ApiContratos.Models;
using ApiContratos.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var contratos = new List<Contrato>()
{
    new Contrato
    {
        Id = 1,
        Numero = "CT-001",
        Cliente = "Cliente A",
        DataInicio = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-2)),
        DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(10)),
        Descricao = "Contrato de manutenção"
    },
    new Contrato
    {
        Id = 2,
        Numero = "CT-002",
        Cliente = "Cliente B",
        DataInicio = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-1)),
        DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(5)),
        Descricao = "Contrato anual de suporte"
    }
};

RotasGET.Map(app, contratos);
RotasPOST.Map(app, contratos);
RotasDELETE.Map(app, contratos);

app.Run();
