using ApiContratos.Models;
using ApiContratos.Routes;
using Microsoft.EntityFrameworkCore;
using ApiContratos.Data;

// Cria o builder da aplicação (configurações iniciais da API)
var builder = WebApplication.CreateBuilder(args);

// Adiciona o banco de dados SQLite e o contexto (classe que gerencia o BD)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=contratos.db"));

// Constrói a aplicação
var app = builder.Build();

// Configura o middleware para servir arquivos estáticos (HTML, CSS, JS, etc.)
app.UseDefaultFiles();   // permite servir index.html por padrão
app.UseStaticFiles();    // serve arquivos estáticos da pasta wwwroot

// Garante que o banco de dados seja criado se ainda não existir
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

RotasGET.Map(app);
RotasPOST.Map(app);
RotasDELETE.Map(app);

app.Run();
