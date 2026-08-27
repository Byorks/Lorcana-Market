using Domain.Context;
using Infrastructure.DTOs.Inputs;
using Worker.Services.Interfaces;

namespace Worker.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ApiDbContext db;

    public TransacaoService(ApiDbContext db)
    {
        this.db = db;
    }
    public Task ProcessarAsync(TransacaoMessageInput transacao, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
