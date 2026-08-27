using Api.Services.Interfaces;
using Infrastructure.DTOs.Inputs;
using Microsoft.AspNetCore.Mvc;

namespace POC_MessageBroker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransacoesController : ControllerBase
{
    private readonly IRabbitMqPublisher _rabbitMqPublisher;

    public TransacoesController(IRabbitMqPublisher rabbitMqPublisher)
    {
        _rabbitMqPublisher = rabbitMqPublisher;
    }

    [HttpPost]
    public async Task<IActionResult> CriarTransacao([FromBody] TransacaoInput transacaoInput, CancellationToken cancellationToken)
    {
        Guid codigoId = Guid.NewGuid();
        var transacao = new TransacaoMessageInput(codigoId, transacaoInput.TipoTransacao, transacaoInput.Valor);

        await _rabbitMqPublisher.PublishAsync(transacao, cancellationToken);

        return Ok();
    }
}
