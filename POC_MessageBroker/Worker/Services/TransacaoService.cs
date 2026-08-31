using Domain;
using Domain.Context;
using Domain.Enums;
using Infrastructure.DTOs.Inputs;
using Microsoft.EntityFrameworkCore;
using Worker.Services.Interfaces;

namespace Worker.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ApiDbContext db;

    public TransacaoService(ApiDbContext db)
    {
        this.db = db;
    }
    public async Task<bool> ProcessarAsync(TransacaoMessageInput transacao, CancellationToken cancellationToken = default)
    {
        try
        {
            // Idempotência, não processar a mesma transação mais de uma vez
            if (await db.Transacoes.AnyAsync(x => x.CodigoId == transacao.CodigoId, cancellationToken))
                return true;

            Transacao? newTransacao = null;

            await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

            if (transacao.TipoTransacao == TipoTransacao.Saque)
            {
                // Caso houver saldo na conta, realiza a alteração do saldo diretamente no campo
                // Isto serve para caso duas instâncias de workers processarem a mesma mensagem, não aconteça de 
                //var updated = await db.Contas
                //    .Where(x => x.Id == transacao.ContaId && x.Saldo >= transacao.Valor) // Procura a conta e verifica se há saldo
                //    .ExecuteUpdateAsync(setters =>
                //        setters.SetProperty(x => x.Saldo, x => x.Saldo - transacao.Valor));

                // Porém, de um jeito mais simples :)
                // Um pouco mais verboso, porém, o saldo é adquirido a partir das transações aprovadas de tipos deposito e saque
                var transacoes = db.Transacoes
                    .AsNoTracking()
                    .Where(x => x.ContaId == transacao.ContaId && x.StatusTransacao == StatusTransacao.Aprovada);

                decimal deposito = transacoes
                    .Where(x => x.StatusTransacao == StatusTransacao.Aprovada && x.TipoTransacao == TipoTransacao.Deposito)
                    .Sum(x => x.Valor);

                decimal saque = transacoes
                    .Where(x => x.StatusTransacao == StatusTransacao.Aprovada && x.TipoTransacao == TipoTransacao.Saque)
                    .Sum(x => x.Valor);

                decimal saldo = deposito - saque;

                //if (updated == 0)
                if (saldo >= transacao.Valor)
                    newTransacao = new Transacao(transacao.CodigoId, 
                                                 transacao.TipoTransacao,
                                                 StatusTransacao.Aprovada, 
                                                 transacao.Valor, 
                                                 transacao.DataHora, 
                                                 transacao.ContaId);
                else
                    newTransacao = new Transacao(transacao.CodigoId, 
                                                 transacao.TipoTransacao, 
                                                 StatusTransacao.Rejeitada, 
                                                 transacao.Valor, 
                                                 transacao.DataHora, 
                                                 transacao.ContaId, 
                                                 "Não há saldo suficiente para realizar esta transação.");
            }
            else
            {
                newTransacao = new Transacao(transacao.CodigoId, transacao.TipoTransacao, StatusTransacao.Aprovada, transacao.Valor, transacao.DataHora, transacao.ContaId);
            }

            await db.Transacoes.AddAsync(newTransacao, cancellationToken);

            await db.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }
}
