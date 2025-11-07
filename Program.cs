using ApiContratos.Models;
using ApiContratos.Routes;

var builder = WebApplication.CreateBuilder(args);

// ✅ Adiciona suporte ao Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Ativa Swagger no modo desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Banco de dados em memória
var contratos = new List<Contrato>()
{
    new Contrato { Id = 1, Numero = "CT-001", Cliente = "Cliente A", DataInicio = DateTime.UtcNow.AddMonths(-2), DataVencimento = DateTime.UtcNow.AddMonths(10), Ativo = true, Descricao = "Contrato de manutenção" },
    new Contrato { Id = 2, Numero = "CT-002", Cliente = "Cliente B", DataInicio = DateTime.UtcNow.AddMonths(-1), DataVencimento = DateTime.UtcNow.AddMonths(5), Ativo = true, Descricao = "Contrato anual de suporte" }
};

// ✅ Registra suas rotas personalizadas
RotasGET.Map(app, contratos);
RotasPOST.Map(app, contratos);
RotasDELETE.Map(app, contratos);

app.Run();
