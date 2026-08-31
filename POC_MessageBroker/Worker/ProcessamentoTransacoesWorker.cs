using Domain.Context;
using Microsoft.Extensions.Hosting;
using Worker.Services.Interfaces;

namespace Worker;

public class ProcessamentoTransacoesWorker : BackgroundService
{
    private readonly IRabbitMqConsumer _consumer;
    public ProcessamentoTransacoesWorker(IRabbitMqConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumer.StartConsumerAsync(stoppingToken);

        // Mantém o Worker vivo até o cancelamento
        //await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
