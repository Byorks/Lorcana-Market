using Domain;
using Domain.Context;
using Infrastructure.DTOs.Inputs;
using Infrastructure.DTOs.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace POC_MessageBroker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContasController : ControllerBase
{
    private readonly ApiDbContext _db;

    public ContasController(ApiDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodas(CancellationToken cancellationToken)
    {
        var contas = await _db.Contas
            .AsNoTracking()
            .Include(x => x.Transacoes)
            .Select(c => new ContaResult { Id = c.Id, Nome = c.Nome, Saldo = c.Saldo })
            .ToListAsync(cancellationToken);

        return Ok(contas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(long id, CancellationToken cancellationToken)
    {
        var conta = await _db.Contas
            .AsNoTracking()
            .Include(x => x.Transacoes)
            .Where(c => c.Id == id)
            .Select(c => new ContaResult { Id = c.Id, Nome = c.Nome, Saldo = c.Saldo })
            .FirstOrDefaultAsync(cancellationToken);

        if (conta is null)
            return NotFound();

        return Ok(conta);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ContaInput contaInput, CancellationToken cancellationToken)
    {
        var conta = new Conta
        {
            Nome = contaInput.Nome
        };

        _db.Contas.Add(conta);
        await _db.SaveChangesAsync(cancellationToken);

        var contaResult = new ContaResult { Id = conta.Id, Nome = conta.Nome };

        return CreatedAtAction(nameof(ObterPorId), new { id = conta.Id }, contaResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(long id, [FromBody] ContaInput contaInput, CancellationToken cancellationToken)
    {
        var contaExistente = await _db.Contas.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (contaExistente is null)
            return NotFound();

        contaExistente.Nome = contaInput.Nome;

        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(long id, CancellationToken cancellationToken)
    {
        var conta = await _db.Contas.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (conta is null)
            return NotFound();

        _db.Contas.Remove(conta);
        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
