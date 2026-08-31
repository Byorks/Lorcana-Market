using Domain.Enums;

namespace Infrastructure.DTOs.Inputs;

public class TransacaoMessageInput
{
    public Guid CodigoId { get; set; } // Idempotência
    public long ContaId { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataHora { get; set; }

    public TransacaoMessageInput(Guid codigoId, long contaId, TipoTransacao tipoTransacao, decimal valor)
    {
        CodigoId = codigoId;
        ContaId = contaId;
        TipoTransacao = tipoTransacao;
        Valor = valor;
        DataHora = DateTime.Now;
    }
}
