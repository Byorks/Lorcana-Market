using Api.Services.Interfaces;
using Domain.Context;
using Infrastructure.DTOs.Inputs;
using Infrastructure.DTOs.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace POC_MessageBroker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransacoesController : ControllerBase
{
    private readonly IRabbitMqPublisher _rabbitMqPublisher;
    private readonly ApiDbContext db;

    public TransacoesController(IRabbitMqPublisher rabbitMqPublisher, ApiDbContext db)
    {
        _rabbitMqPublisher = rabbitMqPublisher;
        this.db = db;
    }

    [HttpPost]
    public async Task<IActionResult> CriarTransacao([FromBody] TransacaoInput transacaoInput, CancellationToken cancellationToken)
    {
        if (!await db.Contas.AnyAsync(x => x.Id == transacaoInput.ContaId, cancellationToken))
            return NotFound("Conta não encontrada");

        if (transacaoInput.Valor <= 0)
            return BadRequest("O valor da transação deve ser maior que zero.");

        Guid codigoId = Guid.NewGuid(); // O Guid normal pode ter uma minima chance de colisão, mas é extremamente improvável.
                                        // O Uuid7 resolveria esse problema, mas nesse caso, podemos utilizar o Guid normal para simplificar a implementação.
        var transacao = new TransacaoMessageInput(codigoId, transacaoInput.ContaId, transacaoInput.TipoTransacao, transacaoInput.Valor);

        await _rabbitMqPublisher.PublishAsync(transacao, cancellationToken);

        return Ok();
    }

    [HttpGet("Conta/{id}")]
    public async Task<IActionResult> RetornarTransacao(long id, CancellationToken cancellationToken)
    {
        var transacoes = db.Transacoes
            .AsNoTracking()
            .Where(x => x.ContaId == id)
            .Select(x => new TransacaoResult
            {
                Id = x.Id,
                ContaId = x.ContaId,
                TipoTransacao = x.TipoTransacao,
                Valor = x.Valor,
                StatusTransacao = x.StatusTransacao,
                CodigoId = x.CodigoId,
                DataHora = x.DataHora,
                Observacao = x.Observacao
            });

        return Ok(transacoes);
    }
}
