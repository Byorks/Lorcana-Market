using Api.DTOs.Inputs;
using Api.Services.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Api.Services;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private ConnectionFactory _factory { get; set; }
    private const string QueueName = "transacoes";

    public RabbitMqPublisher()
    {
        _factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
    }

    public async Task PublishAsync(TransacaoMessageInput transacao, CancellationToken cancellationToken = default)
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
            arguments: null);

        // Serializa a mensagem para json
        var jsonMessage = JsonSerializer.Serialize(transacao);

        // Converte a mensagem para bytes (matriz de bytes, dá pra enviar qualquer coisa por aqui)
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        // Envia a mensagem para a fila
        await channel.BasicPublishAsync(
            exchange: string.Empty, // Não utiliza exchange, publica diretamente na fila
            routingKey: QueueName, // Routingkey precisa ser o nome da fila
            body: body); // Mensagem serializada e codificada em bytes
    }
}
