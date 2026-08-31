using Infrastructure.DTOs.Inputs;

namespace Worker.Services.Interfaces;

public interface ITransacaoService
{
    Task<bool> ProcessarAsync(TransacaoMessageInput transacao, CancellationToken cancellationToken = default);
}
