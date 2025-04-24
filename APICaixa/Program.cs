using APICaixa.Aplicacao.Servicos;
using APICaixa.Dominio.Interfaces;
using APICaixa.Infraestrutura.Repositorios;
using Infraestrutura.Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BDContexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BDCaixa")));

builder.Services.AddScoped<ServicoConta>();
builder.Services.AddScoped<IRepositorioConta, RepositorioConta>();
builder.Services.AddScoped<IRepositorioLog, RepositorioLog>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Caixa",
        Version = "v1",
        Description = "Esta API simula o sistema de um banco, permitindo o cadastro de contas bancárias, consulta, desativação e transferências entre contas.\n\n" +
                      "📌 **Funcionalidades disponíveis:**\n" +
                      "1. Cadastro de conta bancária (com saldo inicial de R$1000)\n" +
                      "2. Consulta por nome ou documento\n" +
                      "3. Desativação de conta (registro com log)\n" +
                      "4. Transferência entre contas ativas com saldo suficiente\n\n" +
                      "⚠️ **Atenção**: Não há autenticação/autorização implementada.\n" +
                      "📂 Repositório GitHub: [link_do_repositorio_aqui]",
        Contact = new OpenApiContact
        {
            Name = "Juliano",
            Email = "julianops79@gmail.com"
        }
    });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
