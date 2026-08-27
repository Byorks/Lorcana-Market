using Infrastructure.DTOs.Inputs;

namespace Api.Services.Interfaces;

public interface IRabbitMqPublisher
{
    Task PublishAsync(TransacaoMessageInput transacao, CancellationToken cancellationToken = default);
}
