using Infrastructure.DTOs.Inputs;

namespace Worker.Services.Interfaces;

public interface ITransacaoService
{
    Task ProcessarAsync(TransacaoMessageInput transacao, CancellationToken cancellationToken = default);
}
