using Api.Services;
using Api.Services.Interfaces;
using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Worker;
using Worker.Services;
using Worker.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Injeção de dependencia do serviço de publicação no RabbitMQ
builder.Services.AddScoped<IRabbitMqPublisher, RabbitMqPublisher>();
builder.Services.AddScoped<IRabbitMqConsumer, RabbitMqConsumer>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();

builder.Services.AddHostedService<ProcessamentoTransacoesWorker>();

builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"),
        x => x.MigrationsAssembly("Domain"));
});

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
