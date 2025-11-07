using ApiContratos.Models;
using ApiContratos.Routes;
using Microsoft.EntityFrameworkCore;
using ApiContratos.Data;

// Cria o builder da aplicação (configurações iniciais da API)
var builder = WebApplication.CreateBuilder(args);

// Adiciona o banco de dados SQLite e o contexto (classe que gerencia o BD)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=contratos.db"));

// Adiciona suporte ao Swagger (documentação/teste da API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Constrói a aplicação
var app = builder.Build();


// Garante que o banco de dados seja criado se ainda não existir
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Ativa o Swagger na aplicação
app.UseSwagger();
app.UseSwaggerUI();


// Redireciona a rota principal ("/") para o Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

RotasGET.Map(app);
RotasPOST.Map(app);
RotasDELETE.Map(app);

app.Run();
