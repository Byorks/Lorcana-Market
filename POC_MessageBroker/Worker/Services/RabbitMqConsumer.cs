using Domain.Context;
using Infrastructure.DTOs.Inputs;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Worker.Services.Interfaces;

namespace Worker.Services;

public class RabbitMqConsumer : IRabbitMqConsumer
{
    private ConnectionFactory _factory { get; set; }
    private readonly IServiceScopeFactory _scopeFactory;
    private const string QueueName = "transacoes";

    public RabbitMqConsumer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;

        _factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
    }

    public async Task StartConsumerAsync(CancellationToken cancellationToken = default)
    {
        // Cria uma conexão com o RabbitMQ
        await using var connection = await _factory.CreateConnectionAsync(cancellationToken);

        // Cria um canal de comunicação com o RabbitMQ
        await using var channel = await connection.CreateChannelAsync();

        // Cria a fila caso não exista
        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) => // ea = event args
        {
            try
            {
                var body = ea.Body.ToArray();

                var jsonMessage = Encoding.UTF8.GetString(body);

                var transacao = JsonSerializer.Deserialize<TransacaoMessageInput>(jsonMessage);

                if (transacao is null)
                    throw new InvalidOperationException("Não foi possível desserializar a mensagem.");

                Console.WriteLine($"Transação recebida: " +
                    $"CódigoId={transacao.CodigoId}, " +
                    $"TipoTransacao={transacao.TipoTransacao}, " +
                    $"Valor={transacao.Valor}");

                // TODO: Processar a mensagem aqui

                await using var scope =
                    _scopeFactory.CreateAsyncScope();

                var service =
                    scope.ServiceProvider.GetRequiredService<ITransacaoService>();

                await service.ProcessarAsync(transacao);

                // Confirma que a mensagem foi processada com sucesso
                await channel.BasicAckAsync(
                    deliveryTag: ea.DeliveryTag,
                    multiple: false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar a mensagem: " + ex.Message);

                // Não foi possível processar, recoloca a mensagem na fila para tentar novamente
                await channel.BasicNackAsync(
                    deliveryTag: ea.DeliveryTag,
                    multiple: false,
                    requeue: true);
            }
        };

        // Começa a consumir aqui
        // Aqui, basicamente nós dizemos ao RabbitMQ: "Quero que você me avise sempre que chegar uma mensagem nessa fila, e quando isso acontecer, execute o evento consumer.ReceivedAsync"
        // Fica funcionando continuamente
        await channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false, // RabbitMQ não considera a mensagem automaticamente processada, precisamos confirmar manualmente
            consumer: consumer,
            cancellationToken: cancellationToken);
    }
}
