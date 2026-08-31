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
builder.Services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>(); // Necessário o consumer ser singleton, pois o worker é singleton e o consumer também precisa ser singleton para não criar múltiplas conexões com o RabbitMQ.
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
