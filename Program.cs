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
        Cliente = "Luana Nunes",
        DataInicio = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-2)),
        DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(10)),
        Descricao = "Contrato de manutenção"
    },
    new Contrato
    {
        Id = 2,
        Numero = "CT-002",
        Cliente = "CCláudio Dias",
        DataInicio = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-50)),
        DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-25)),
        Descricao = "Contrato anual de suporte"
    },
    new Contrato
    {
        Id = 3,
        Numero = "CT-003",
        Cliente = "Tiago Alves",
        DataInicio = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-8)),
        DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(2)),
        Descricao = "Contrato Empresarial"
    },
    new Contrato
    {
        Id = 4,
        Numero = "CT-004",
        Cliente = "Sônia Souza",
        DataInicio = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-95)),
        DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-32)),
        Descricao = "Contrato anual de consultoria"
    },
    new Contrato
    {
        Id = 5,
        Numero = "CT-256",
        Cliente = "Alexandre Lopes",
        DataInicio = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-4)),
        DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(8)),
        Descricao = "Contrato de desenvolvimento"
    },
    new Contrato
    {
        Id = 6,
        Numero = "CT-005",
        Cliente = "Márcia Alves",
        DataInicio = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-1)),
        DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(5)),
        Descricao = "Contrato semestral de treinamento"
    }
};

RotasGET.Map(app, contratos);
RotasPOST.Map(app, contratos);
RotasDELETE.Map(app, contratos);

app.Run();
