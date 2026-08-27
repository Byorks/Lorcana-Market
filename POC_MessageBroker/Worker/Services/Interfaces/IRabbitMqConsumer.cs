namespace Worker.Services.Interfaces;

public interface IRabbitMqConsumer
{
    Task StartConsumerAsync(CancellationToken cancellationToken = default);
}
